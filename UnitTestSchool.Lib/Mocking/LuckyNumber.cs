namespace UnitTestSchool.Lib.Mocking;

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

public class MyUnitOfWork : IMyUnitOfWork, IDisposable
{
	public INumberRepository Number => throw new NotImplementedException();

	public void Complete() => throw new NotImplementedException();

	public void Dispose() => throw new NotImplementedException();
}

public interface INumberRepository
{
	Task AddNumber(RandomNumber randomNumber);
}

public class LuckyNumber
{
	//private readonly Random _random = new();
	private readonly IRandomWrapper _randomWrapper;

	private readonly IConsoleWrapper _consoleWrapper;
	private readonly IMyDateTimeWrapper _myDateTimeWrapper;
	private readonly IMyUnitOfWork _myUnitOfWork;

	public LuckyNumber(IRandomWrapper randomWrapper, IConsoleWrapper consoleWrapper, IMyDateTimeWrapper myDateTimeWrapper)
	{
		_randomWrapper = randomWrapper;
		_consoleWrapper = consoleWrapper;
		_myDateTimeWrapper = myDateTimeWrapper;
	}

	public int Generate()
	{
		//var luckyNumber = _random.Next(100);
		var luckyNumber = _randomWrapper.Next(100);

		//using (var context = new ApplicationDbContext())
		using (var context = new MyUnitOfWork())
		{
			context.Number.AddNumber(
				new RandomNumber
				{
					Number = luckyNumber,
					//Date = DateTime.UtcNow
					Date = _myDateTimeWrapper.UtcNow
				});
		}

		//Console.WriteLine($"Szczęśliwa liczba to: {luckyNumber}");
		_consoleWrapper.WriteLine($"Szczęśliwa liczba to: {luckyNumber}");

		return luckyNumber;
	}
}