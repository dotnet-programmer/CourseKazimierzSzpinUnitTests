using NUnit.Framework;
namespace UnitTestSchool.UnitTests.Basics;

public class CalculatorTests
{
	private Calculator _calculator;

	// wykonywane jednorazowo przed zestawem testów
	[OneTimeSetUp]
	public void OneTimeSetUp() => _calculator = null;

	// wykonywane przed każdą metodą testową
	[SetUp]
	public void SetUp() => _calculator = new();

	// wykonywane jednorazowo po zestawie testów
	[OneTimeTearDown]
	public void OneTimeTearDown() => _calculator = null;

	// wykonywane po każdej metodzie testowej
	[TearDown]
	public void TearDown() => _calculator = null;

	// metoda inicjalizująca - można pisać swoje metody wywoływane w Arrange zamiast metody oznaczonej atrybutem SetUp
	// różnica to trzeba pamiętać o wywoływaniu ich ręcznie za każdym razem
	// metody SetUp działają dla wszystkich metod, a metody inicjalizujące można wywołać tylko w wybranych metodach testowych
	private void Init() => _calculator = new();

	// NazwaMetody_Scenariusz_OczekiwanyRezultat
	[Test]
	public void Add_WhenCalled_ShouldReturnSum()
	{
		// Arrange - przygotowanie obiektów (inicjalizacje, mogą być w metodach inicjalizujacych)
		//Calculator calculator = new();
		Init();

		// Act - działanie - wywołanie metody testowanej
		int result = _calculator.Add(1, 2);

		// Assert - weryfikacja działania metody testowanej - sprawdzanie otrzymanego wyniku z wynikiem oczekiwanym
		Assert.AreEqual(3, result);
	}

	// NuGet - FluentAssertions
	[Test]
	public void Add_WhenCalled_ShouldReturnSum_FluentAssertions()
	{
		//Calculator calculator = new();
		int result = _calculator.Add(1, 2);
		result.Should().Be(3);

		//tylko 1 logiczna asercja na test
		//calculator.Sum.Should().Be(3); // powinien być kolejny test z odpowiednią nazwą
	}

	[Test]
	public void Subtraction_WhenCalled_ShouldReturnSubtraction()
	{
		int result = _calculator.Subtraction(1, 2);
		result.Should().Be(-1);
	}

	// zamiast pisać kilka testów z innymi danymi dla 1 metody, należy napisać metodę - test paramteryzowany
	//[Test]
	//public void Add_TwoPositiveNumbers_ShouldReturnSum()
	//{
	//	int result = _calculator.Add(1, 2);
	//	result.Should().Be(3);
	//}
	//[Test]
	//public void Add_TwoNegativeNumbers_ShouldReturnSum()
	//{
	//	int result = _calculator.Add(-1, -2);
	//	result.Should().Be(-3);
	//}
	//[Test]
	//public void Add_OnePositiveOneNegativeNumber_ShouldReturnSum()
	//{
	//	int result = _calculator.Add(1, -2);
	//	result.Should().Be(-1);
	//}

	// testy parametryzowane
	[TestCase(1, 2, 3)]
	[TestCase(-1, -2, -3)]
	[TestCase(1, -2, -1)]
	public void Add_WhenCalled_ShouldReturnSum(int number1, int number2, int expectedResult)
	{
		int result = _calculator.Add(number1, number2);
		result.Should().Be(expectedResult);
	}


	[Test]
	public void Divide_WhenCalled_ShouldReturnDivision()
	{
		var result = _calculator.Divide(4, 2);
		result.Should().Be(2);
	}

	[Test]
	public void Divide_WhenDivisorIsZero_ShouldThrowDivideByZeroException()
	{
		Action action = () => _calculator.Divide(4, 0);
		action.Should().ThrowExactly<DivideByZeroException>();
	}
}