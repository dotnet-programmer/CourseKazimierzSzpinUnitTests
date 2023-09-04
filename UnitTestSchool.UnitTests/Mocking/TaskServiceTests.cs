using Task = UnitTestSchool.Lib.Mocking.Task;

namespace UnitTestSchool.UnitTests.Mocking;

internal class TaskServiceTests
{
	private Mock<ILogger> _mockLogger;
	private Mock<IUnitOfWork> _mockUnitOfWork;
	private Mock<IEmailSender> _mockEmailSender;
	private Mock<IMessageBoxWrapper> _mockMessageBoxWrapper;
	private TaskService _taskService;
	private Task _task;

	[SetUp]
	public void SetUp()
	{
		_task = new Task { Id = 1, Title = "1", User = new User { Email = "mail@mail.com" } };

		_mockUnitOfWork = new Mock<IUnitOfWork>();
		_mockUnitOfWork.Setup(x => x.Task.GetTask(_task.Id)).Returns(_task);

		_mockLogger = new Mock<ILogger>();
		_mockEmailSender = new Mock<IEmailSender>();
		_mockMessageBoxWrapper = new Mock<IMessageBoxWrapper>();

		_taskService = new(_mockLogger.Object, _mockUnitOfWork.Object, _mockEmailSender.Object, _mockMessageBoxWrapper.Object);
	}

	[Test]
	public void CloseTask_WhenTaskNotFound_ShouldThrowException()
	{
		_mockUnitOfWork.Setup(x => x.Task.GetTask(1)).Returns((Task)null);
		Action action = () => _taskService.CloseTask(1);
		action.Should().ThrowExactly<Exception>().WithMessage("*Not found task.*");
	}

	[Test]
	public void CloseTask_WhenTaskIsAlreadyClosed_ShouldThrowException()
	{
		_task.IsClosed = true;
		Action action = () => _taskService.CloseTask(1);
		action.Should().ThrowExactly<Exception>().WithMessage("*The task is already closed.*");
	}

	[Test]
	public void CloseTask_WhenEmailFails_ShouldLogError()
	{
		_mockEmailSender.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
		_taskService.CloseTask(_task.Id);
		_mockLogger.Verify(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
	}

	[Test]
	public void CloseTask_WhenEmailFails_ShouldDisplayMessage()
	{
		_mockEmailSender.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
		_taskService.CloseTask(_task.Id);
		_mockMessageBoxWrapper.Verify(x => x.Show(It.IsAny<string>()), Times.Once);
	}

	[Test]
	public void CloseTask_WhenCalled_ShouldUpdateTaskInDatabase()
	{
		_taskService.CloseTask(_task.Id);
		_task.IsClosed.Should().BeTrue();
		_mockUnitOfWork.Verify(x => x.Complete(), Times.Once);
	}

	[Test]
	public void CloseTask_WhenCalled_ShouldSendEmail()
	{
		_taskService.CloseTask(_task.Id);
		_mockEmailSender.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), _task.User.Email), Times.Once);
	}

	[Test]
	public void CloseTask_WhenCalled_ShouldDisplayMessage()
	{
		_taskService.CloseTask(_task.Id);
		_mockMessageBoxWrapper.Verify(x => x.Show(It.IsAny<string>()), Times.Once);
	}
}