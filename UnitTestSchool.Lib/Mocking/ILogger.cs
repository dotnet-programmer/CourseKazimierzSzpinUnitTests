namespace UnitTestSchool.Lib.Mocking;

public interface ILogger
{
	void Error(Exception exception, string message);
}