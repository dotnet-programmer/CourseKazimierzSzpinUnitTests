using Microsoft.Extensions.Logging;

namespace OrdersManager.UnitTests.Extensions;

public static class LoggerMockExtensions
{
	public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel logLevel, string expectedMessage, Times times)
		=> loggerMock.Verify(
			x => x.Log(
				logLevel,
				It.IsAny<EventId>(),
				It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(expectedMessage)),
				It.IsAny<Exception>(),
				It.IsAny<Func<It.IsAnyType, Exception, string>>()),
			times);
}