using System.Text.Json;

namespace UnitTestSchool.UnitTests.Mocking;

internal class ConfigHelperTests
{
	private Mock<IFileReader> _mockFileReader;
	private ConfigHelper _configHelper;

	[SetUp]
	public void SetUp()
	{
		_mockFileReader = new Mock<IFileReader>();
		_configHelper = new(_mockFileReader.Object);
	}

	[Test]
	public void GetConnectionString_WhenCalled_ShouldReturnConnectionString()
	{
		// przygotowanie wartości oczekiwanej
		Config config = new() { ConnectionString = "1" };

		// przygotowanie Mocka zastępującego FileReader
		string jsonConfig = JsonSerializer.Serialize(config);
		_mockFileReader.Setup(x => x.Read(It.IsAny<string>())).Returns(jsonConfig);

		string connectionString = _configHelper.GetConnectionString();

		connectionString.Should().Be(config.ConnectionString);
	}

	[Test]
	public void GetConnectionString_WhenConfigIsNull_ShouldThrowExceptionWithCorrectMessage()
	{
		_mockFileReader.Setup(x => x.Read(It.IsAny<string>())).Returns("");

		Action action = () => _configHelper.GetConnectionString();

		action.Should().ThrowExactly<Exception>().WithMessage("*Incorrect parsing config*");
	}
}