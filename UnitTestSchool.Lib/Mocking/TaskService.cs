using NLog;

namespace UnitTestSchool.Lib.Mocking;

public class TaskService
{
	private readonly Logger _logger = LogManager.GetCurrentClassLogger();
	private readonly ApplicationDbContext _context;

	public TaskService() => _context = new ApplicationDbContext();

	public void CloseTask(int taskId)
	{
		var task = _context.Tasks.FirstOrDefault(x => x.Id == taskId);

		task.IsClosed = true;
		_context.SaveChanges();

		try
		{
			new EmailSender().Send(
				$"Zadanie {task.Title}",
				$"Zadanie {task.Title} zostało zamknięte.",
				task.User.Email);

			//MessageBox.Show("Wysyłanie e-mail'a zakończono sukcesem.");
		}
		catch (Exception exception)
		{
			_logger.Error(exception, $"Wysyłanie e-mail zakończone błędem {exception.Message}.");
			//MessageBox.Show("Wysyłanie e-mail'a zakończono błędem.");
		}
	}
}
