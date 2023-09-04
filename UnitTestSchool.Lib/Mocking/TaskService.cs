namespace UnitTestSchool.Lib.Mocking;

public class TaskService
{
	private readonly ILogger _logger;

	// private ApplicationDbContext _context;
	private readonly IUnitOfWork _unitOfWork;

	private readonly IEmailSender _emailSender;
	private readonly IMessageBoxWrapper _messageBoxWrapper;

	public TaskService(ILogger logger, IUnitOfWork unitOfWork, IEmailSender emailSender, IMessageBoxWrapper messageBoxWrapper)
	{
		_logger = logger;
		// _context = new ApplicationDbContext();
		_unitOfWork = unitOfWork;
		_emailSender = emailSender;
		_messageBoxWrapper = messageBoxWrapper;
	}

	public void CloseTask(int taskId)
	{
		// var task = _context.Tasks.FirstOrDefault(x => x.Id == taskId);
		var task = _unitOfWork.Task.GetTask(taskId);

		if (task is null)
		{
			throw new Exception("Not found task.");
		}

		if (task.IsClosed)
		{
			throw new Exception("The task is already closed.");
		}

		task.IsClosed = true;

		// _context.SaveChanges();
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