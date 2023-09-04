namespace UnitTestSchool.Lib.Mocking;

public class Robot
{
	public string Greetings()
	{
		if (DateTime.Now.Hour < 18)
		{
			return "Dzień dobry!";
		}

		return "Dobry wieczór!";
	}
}
