using UnitTestSchool.Lib.Mocking;

namespace UnitTestSchool.UnitTests.Mocking;

internal class LuckyNumberTests
{
	private Mock<IRandomWrapper> _mockRandomWrapper;
	private Mock<IConsoleWrapper> _mockConsoleWrapper;
	private Mock<IMyDateTimeWrapper> _mockMyDateTimeWrapper;
	private Mock<IMyUnitOfWork> _mockMyUnitOfWork;
	private LuckyNumber _luckyNumber;
	private RandomNumber _randomNumber;

	[SetUp]
	public void SetUp()
	{
		_mockRandomWrapper = new Mock<IRandomWrapper>();
		_mockRandomWrapper.Setup(x=>x.Next(It.IsAny<int>())).Returns(1);

		_mockConsoleWrapper = new Mock<IConsoleWrapper>();
		_mockMyDateTimeWrapper = new Mock<IMyDateTimeWrapper>();
		_mockMyUnitOfWork = new Mock<IMyUnitOfWork>();
		_randomNumber = new RandomNumber { Date = new DateTime(2023, 5, 5), Number = 1 };
		_mockMyUnitOfWork.Setup(x => x.Number.AddNumber(_randomNumber));

		_luckyNumber = new(_mockRandomWrapper.Object, _mockConsoleWrapper.Object, _mockMyDateTimeWrapper.Object, _mockMyUnitOfWork.Object);
	}

	[Test]
	public void Generate_WhenCalled_ShouldWriteMessageAndReturnNumber()
	{
		var result = _luckyNumber.Generate();
		result.Should().Be(1);
		_mockConsoleWrapper.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once);
	}
}