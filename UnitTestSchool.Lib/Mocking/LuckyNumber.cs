namespace UnitTestSchool.Lib.Mocking;

public class LuckyNumberOld
{
	// tak jak DateTime.Now - przy każdym uruchomieniu testu wynik będzie inny, więc to jest zewnętrzna zależność do usunięcia
	private readonly Random _random = new();

	public int Generate()
	{
		var luckyNumber = _random.Next(100);

		// zewnętrzna zależność - baza danych
		using (ApplicationDbContext context = new())
		{
			context.Numbers.Add(
				new RandomNumber
				{
					Number = luckyNumber,
					// zewnętrzna zależność - czas
					Date = DateTime.UtcNow
				});
		}

		// zewnętrzna zależność - uzależnienie od konsoli
		Console.WriteLine($"Szczęśliwa liczba to: {luckyNumber}");

		return luckyNumber;
	}
}

// Refaktoryzacja:

public interface IRandomWrapper
{
	int Next(int maxValue);
}

public interface IConsoleWrapper
{
	void WriteLine(string value);
}

public interface IMyDateTimeWrapper
{
	DateTime UtcNow { get; }
}

public interface IMyUnitOfWork
{
	INumberRepository Number { get; }

	void Complete();
}

public interface INumberRepository
{
	void AddNumber(RandomNumber randomNumber);
}

//public class MyUnitOfWork : IMyUnitOfWork, IDisposable
//{
//	public INumberRepository Number => throw new NotImplementedException();

//	public void Complete() => throw new NotImplementedException();

//	public void Dispose() => throw new NotImplementedException();
//}

public class LuckyNumber(IRandomWrapper randomWrapper, IConsoleWrapper consoleWrapper, IMyDateTimeWrapper myDateTimeWrapper, IMyUnitOfWork myUnitOfWork)
{
	private readonly IRandomWrapper _randomWrapper = randomWrapper;
	private readonly IConsoleWrapper _consoleWrapper = consoleWrapper;
	private readonly IMyDateTimeWrapper _myDateTimeWrapper = myDateTimeWrapper;
	private readonly IMyUnitOfWork _myUnitOfWork = myUnitOfWork;

	public int Generate()
	{
		var luckyNumber = _randomWrapper.Next(100);
		_myUnitOfWork.Number.AddNumber(new RandomNumber
		{
			Number = luckyNumber,
			Date = _myDateTimeWrapper.UtcNow
		});
		_consoleWrapper.WriteLine($"Szczęśliwa liczba to: {luckyNumber}");
		return luckyNumber;
	}
}