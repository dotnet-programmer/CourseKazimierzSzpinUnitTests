namespace UnitTestSchool.Lib.Basics;

public class Math
{
	public int GetNwd(int number1, int number2)
	{
		while (number1 != number2)
		{
			if (number1 > number2)
			{
				number1 -= number2;
			}
			else
			{
				number2 -= number1;
			}
		}

		return number1;
	}

	// jeśli parametr typu int, to testy powinny uwzględniać 3 przypadki: liczby ujemne, zero i dodatnie
	public IEnumerable<int> GetEvenNumbers(int range)
	{
		List<int> evenNumbers = [];

		for (int i = 1; i <= range; i++)
		{
			if (i % 2 == 0)
			{
				evenNumbers.Add(i);
			}
		}

		return evenNumbers;
	}

	public string GetEvenOrOddMsg(int number)
		=> number % 2 == 0 ? "Even" : "Odd";
}