using FluentAssertions;
using NUnit.Framework;
using OrdersManager.Simple;

namespace OrdersManager.UnitTests.Simple
{
	public class FizzBuzzTests
	{
		private FizzBuzz _fizzBuzz;

		[SetUp]
		public void Setup()
			=> _fizzBuzz = new FizzBuzz();

		[Test]
		public void GetOutput_WhenInputIsDivisibleOnlyBy3_ShouldReturnFizz()
		{
			var result = _fizzBuzz.GetOutput(3);

			result.Should().Be("Fizz");
		}

		[Test]
		public void GetOutput_WhenInputIsDivisibleOnlyBy5_ShouldReturnBuzz()
		{
			var result = _fizzBuzz.GetOutput(5);

			result.Should().Be("Buzz");
		}

		[Test]
		public void GetOutput_WhenInputIsDivisibleBy3And5_ShouldReturnFizzBuzz()
		{
			var result = _fizzBuzz.GetOutput(15);

			result.Should().Be("FizzBuzz");
		}

		[Test]
		public void GetOutput_WhenInputIsNotDivisibleBy3And5_ShouldReturnInput()
		{
			var result = _fizzBuzz.GetOutput(1);

			result.Should().Be("1");
		}
	}
}