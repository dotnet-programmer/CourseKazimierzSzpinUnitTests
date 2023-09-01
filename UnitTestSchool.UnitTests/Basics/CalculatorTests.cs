using FluentAssertions;
using NUnit.Framework;
using UnitTestSchool.Lib.Basics;

namespace UnitTestSchool.UnitTests.Basics;

public class CalculatorTests
{
	// NazwaMetody_Scenariusz_OczekiwanyRezultat
	[Test]
	public void Add_WhenCalled_ShouldReturnSum()
	{
		// Arrange - przygotowanie obiektów (inicjalizacje, mogą być w metodach inicjalizujacych)
		Calculator calculator = new();

		// Act - działanie - wywołanie metody testowanej
		int result = calculator.Add(1, 2);

		// Assert - weryfikacja działania metody testowanej - sprawdzanie otrzymanego wyniku z wynikiem oczekiwanym
		Assert.AreEqual(3, result);
	}

    [Test]
	public void Add_WhenCalled_ShouldReturnSum_FluentAssertions()
	{
		Calculator calculator = new();
		int result = calculator.Add(1, 2);
		result.Should().Be(3);

		//tylko 1 logiczna asercja na test
		//calculator.Sum.Should().Be(3); // powinien być kolejny test z odpowiednią nazwą
	}
}