namespace UnitTestSchool.Lib.Mocking;

public class LuckyNumber
{
	private readonly Random _random = new();

	public int Generate()
	{
		var luckyNumber = _random.Next(100);

		using (var context = new ApplicationDbContext())
		{
			context.Numbers.Add(
				new RandomNumber
				{
					Number = luckyNumber,
					Date = DateTime.UtcNow
				});
		}

		Console.WriteLine($"Szczęśliwa liczba to: {luckyNumber}");

		return luckyNumber;
	}
}
