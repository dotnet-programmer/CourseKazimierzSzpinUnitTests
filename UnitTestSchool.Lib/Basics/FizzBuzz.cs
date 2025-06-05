namespace UnitTestSchool.Lib.Basics;

public class FizzBuzz
{
	public static string GetOutputOld(int number)
	{
		if ((number % 3 == 0) && (number % 5 == 0))
		{
			return "FizzBuzz";
		}

		if (number % 3 == 0)
		{
			return "Fizz";
		}

		if (number % 5 == 0)
		{
			return "Buzz";
		}

		return number.ToString();
	}

	public static string GetOutput(int number) 
		=> (number % 3 == 0) && (number % 5 == 0) ? "FizzBuzz" :
			   number % 3 == 0 ? "Fizz" :
			   number % 5 == 0 ? "Buzz" :
			   number.ToString();
}