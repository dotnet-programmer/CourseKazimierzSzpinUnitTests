namespace UnitTestSchool.UnitTests.Mocking;

internal class RobotTests
{
	[TestCase("2023-01-01 17:59", "Dzień dobry!")]
	[TestCase("2023-01-01 18:00", "Dobry wieczór!")]
	[TestCase("2023-01-01 18:01", "Dobry wieczór!")]
	public void Greetings_WhenCalled_ShouldReturnCorrectMessage(DateTime dateTime, string expectedMessage)
	{
		// właściwość Now ma zwracać datę przekazaną w parametrze
		//IDateTimeWrapper mockDateTimeWrapper = Mock.Of<IDateTimeWrapper>(x => x.Now == dateTime);
		//Robot robot = new(mockDateTimeWrapper);

		Mock<IDateTimeWrapper> mockDateTimeWrapper = new();
		mockDateTimeWrapper.Setup(x => x.Now).Returns(dateTime);
		Robot robot = new(mockDateTimeWrapper.Object);

		string result = robot.Greetings();
		result.Should().Be(expectedMessage);
	}
}