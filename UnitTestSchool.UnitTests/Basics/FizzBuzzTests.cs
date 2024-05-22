namespace UnitTestSchool.UnitTests.Basics;

internal class FizzBuzzTests
{
	[Test]
	public void GetOutput_WhenInputIsDivisibleBy3And5_ShouldReturnFizzBuzz()
	{
		var result = FizzBuzz.GetOutput(15);
		result.Should().Be("FizzBuzz");
	}

	[Test]
	public void GetOutput_WhenInputIsDivisibleOnlyBy3_ShouldReturnFizz()
	{
		var result = FizzBuzz.GetOutput(3);
		result.Should().Be("Fizz");
	}

	[Test]
	public void GetOutput_WhenInputIsDivisibleOnlyBy5_ShouldReturnBuzz()
	{
		var result = FizzBuzz.GetOutput(5);
		result.Should().Be("Buzz");
	}

	[Test]
	public void GetOutput_WhenInputIsNotDivisibleOnlyBy3Or5_ShouldReturnInput()
	{
		var result = FizzBuzz.GetOutput(4);
		result.Should().Be("4");
	}
}