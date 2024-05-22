using Math = UnitTestSchool.Lib.Basics.Math;

namespace UnitTestSchool.UnitTests.Basics;

internal class MathTests
{
	private Math _math;

	[SetUp]
	public void SetUp()
		=> _math = new();

	[Test]
	public void GetNwd_WhenCalled_ShouldReturnNdw()
	{
		var result = _math.GetNwd(3, 6);
		result.Should().Be(3);
		//result.Should().BeGreaterThanOrEqualTo(3);
		//result.Should().BeLessThanOrEqualTo(3);
		//result.Should().BeGreaterThan(2);
		//result.Should().BeLessThan(4);
		//result.Should().NotBe(2);
		//result.Should().BePositive();
		//result.Should().BeNegative();
		//result.Should().BeInRange(1, 3);
		//result.Should().NotBeInRange(4, 6);
	}

	// testowanie kolekcji
	[Test]
	public void GetEvenNumbers_WhenCalled_ShouldReturnEvenNumbersInGivenRange()
	{
		int[] expectation = [2, 4];

		var result = _math.GetEvenNumbers(4);
		//result.Should().NotBeEmpty();
		//result.Should().HaveCount(2);
		//result.Should().BeEquivalentTo(expectation);

		// warunki asercji można łączyć ze sobą poprzez And
		result.Should().NotBeEmpty()
			.And.HaveCount(2)
			.And.BeEquivalentTo(expectation);

		//result.Should().Contain(2);
		// jeżeli sprawdzenie czy zawiera więcej niż 1 konkretna liczba, to trzeba przekazać obiekt IEnumerable
		//result.Should().Contain(new[] { 2, 4 });
		//result.Should().NotBeEquivalentTo(new[] { 1, 3 });
		//result.Should().OnlyHaveUniqueItems();
		//result.Should().HaveCountGreaterThan(1);
		//result.Should().HaveCountLessThan(3);
		//result.Should().StartWith(2);
		//result.Should().EndWith(4);
		//result.Should().BeInAscendingOrder();
		//result.Should().BeInDescendingOrder();
		//result.Should().NotBeNull();
	}

	[TestCase(1, "Odd")]
	[TestCase(2, "Even")]
	[TestCase(3, "Odd")]
	[TestCase(4, "Even")]
	public void GetEvenOrOddMsg_WhenCalled_ShouldReturnCorrectMsg(int number, string message)
	{
		var msg = _math.GetEvenOrOddMsg(number);
		msg.Should().Be(message);
	}
}