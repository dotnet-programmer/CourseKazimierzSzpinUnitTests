namespace OrdersManager.WebApp.Simple;

public class FizzBuzz
{
	public string GetOutput(int number)
		=> (number % 3 == 0 && number % 5 == 0) ? "FizzBuzz" :
		   (number % 3 == 0) ? "Fizz" :
		   (number % 5 == 0) ? "Buzz" :
		   number.ToString();
}