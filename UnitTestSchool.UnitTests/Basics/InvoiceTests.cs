namespace UnitTestSchool.UnitTests.Basics;

internal class InvoiceTests
{
	private Invoice _invoice;

	[SetUp]
	public void SetUp()
		=> _invoice = new();

	// testowanie wyjątków
	[Test]
	public void AddPosition_WhenPositionIsNull_ShouldThrowArgumentNullException()
	{
		Action action = () => _invoice.AddPosition(null);

		// ThrowExactly - musi zostać zwrócony konkretny wyjątek
		action.Should().ThrowExactly<ArgumentNullException>().WithMessage("*position*");
	}

	[Test]
	public void AddPosition_WhenPositionIsUnavailable_ShouldThrowException()
	{
		Action action = () => _invoice.AddPosition(new InvoicePosition { IsAvailable = false });

		// Throw<Exception> - może zostać zwrócony ten sam wyjątek, lub wyjątki dziedziczące po nim
		action.Should().Throw<Exception>().WithMessage("*position unavailable*");

		// można sprawdzić czy nie został rzucony wyjątek
		//action.Should().NotThrow();

		// można sprawdzić czy nie został rzucony konkretny wyjątek
		//action.Should().NotThrow<Exception>();
	}
}