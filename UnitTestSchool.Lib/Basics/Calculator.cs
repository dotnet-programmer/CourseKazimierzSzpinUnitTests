namespace UnitTestSchool.Lib.Basics;

public class Calculator
{
    public int Sum { get; set; }

	//public int Add(int number1, int number2) => number1 + number2;

	public int Add(int number1, int number2)
	{
		var result = number1 + number2;
		Sum += result;
		return result;
	}

	public int Subtraction(int number1, int number2)
	{
		return number1 - number2;
	}

	public int Divide(int dividend, int divisor)
	{
		if (divisor == 0)
		{
			throw new DivideByZeroException();
		}

		return dividend / divisor;
	}
}