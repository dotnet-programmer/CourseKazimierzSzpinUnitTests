using NLog;

namespace UnitTestSchool.Lib.Mocking;

public class TaskServiceOld
{
	// uzależnienie od klasy Logger - zewnętrzna zależność
	private readonly Logger _logger = LogManager.GetCurrentClassLogger();

	// uzależnienie od klasy ApplicationDbContext - zewnętrzna zależność
	private readonly ApplicationDbContext _context;

	public TaskServiceOld()
		=> _context = new ApplicationDbContext();

	public void CloseTask(int taskId)
	{
		var task = _context.Tasks.FirstOrDefault(x => x.Id == taskId);

		task.IsClosed = true;
		_context.SaveChanges();

		try
		{
			// użycie konkretnej klasy do wysyłki email spowoduje wysłanie emaila podczas każdego uruchomoienia testów - zewnętrzna zależność
			new EmailSender().Send(
				$"Zadanie {task.Title}",
				$"Zadanie {task.Title} zostało zamknięte.",
				task.User.Email);

			// uzależnienie od biblioteki System.Windows.Forms - zewnętrzna zależność
			//MessageBox.Show("Wysyłanie e-mail'a zakończono sukcesem.");
		}
		catch (Exception exception)
		{
			_logger.Error(exception, $"Wysyłanie e-mail zakończone błędem {exception.Message}.");
			//MessageBox.Show("Wysyłanie e-mail'a zakończono błędem.");
		}
	}
}

// refaktoring:
public class TaskService(ILogger logger, IUnitOfWork unitOfWork, IEmailSender emailSender, IMessageBoxWrapper messageBoxWrapper)
{
	private readonly ILogger _logger = logger;

	// wzorzec Repository i Unit Of Work
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	private readonly IEmailSender _emailSender = emailSender;
	private readonly IMessageBoxWrapper _messageBoxWrapper = messageBoxWrapper;

	public void CloseTask(int taskId)
	{
		var task = _unitOfWork.Task.GetTask(taskId) ?? throw new Exception("Not found task.");

		if (task.IsClosed)
		{
			throw new Exception("The task is already closed.");
		}

		task.IsClosed = true;
		_unitOfWork.Complete();

		try
		{
			_emailSender.Send(
				$"Zadanie {task.Title}",
				$"Zadanie {task.Title} zostało zamknięte.",
				task.User.Email);

			_messageBoxWrapper.Show("Wysyłanie e-mail'a zakończono sukcesem.");
		}
		catch (Exception exception)
		{
			_logger.Error(exception, $"Wysyłanie e-mail zakończone błędem {exception.Message}.");
			_messageBoxWrapper.Show("Wysyłanie e-mail'a zakończono błędem.");
		}
	}
}