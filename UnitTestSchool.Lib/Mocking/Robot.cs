namespace UnitTestSchool.Lib.Mocking;

public class Robot
{
	private readonly IDateTimeWrapper _dateTimeWrapper;

	public Robot(IDateTimeWrapper dateTimeWrapper) => _dateTimeWrapper = dateTimeWrapper;

	public string Greetings() => _dateTimeWrapper.Now.Hour < 18 ? "Dzień dobry!" : "Dobry wieczór!";
}