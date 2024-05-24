namespace UnitTestSchool.Lib.Mocking;

public class Robot
{
	// klasa Robot przed refaktoringiem
	// w tym przypadku wynik testu będzie zależał od godziny uruchomienia, więc jest to zewnętrzna zależność, która musi zostać usunięta
	public string GreetingsOld()
		=> DateTime.Now.Hour < 18
			? "Dzień dobry!"
			: "Dobry wieczór!";


	// klasa przygotowana do testów:

	private readonly IDateTimeWrapper _dateTimeWrapper;

	public Robot(IDateTimeWrapper dateTimeWrapper)
		=> _dateTimeWrapper = dateTimeWrapper;

	public string Greetings()
		=> _dateTimeWrapper.Now.Hour < 18 
		? "Dzień dobry!" 
		: "Dobry wieczór!";
}