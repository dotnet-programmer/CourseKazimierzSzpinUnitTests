namespace UnitTestSchool.UnitTests.Basics;

internal class FizzBuzzTests
{
	[Test]
	public void GetOutput_WhenInputIsDivisibleBy3And5_ShouldReturnFizzBuzz()
		=> FizzBuzz.GetOutput(15).Should().Be("FizzBuzz");
	//var result = FizzBuzz.GetOutput(15);
	//result.Should().Be("FizzBuzz");

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

	[TestCase(15, "FizzBuzz")]
	[TestCase(5, "Buzz")]
	[TestCase(3, "Fizz")]
	[TestCase(4, "4")]
	public void GetOutput_WhenCalled(int input, string expectedResult)
	{
		var result = FizzBuzz.GetOutput(input);
		result.Should().Be(expectedResult);
	}
}