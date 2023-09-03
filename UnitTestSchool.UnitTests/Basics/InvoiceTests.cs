namespace UnitTestSchool.UnitTests.Basics;

internal class InvoiceTests
{
	private Invoice _invoice;

	[SetUp]
	public void SetUp() => _invoice = new();

	// testowanie wyjątków
	[Test]
	public void AddPosition_WhenPositionIsNull_ShouldThrowArgumentNullException()
	{
		Action action = () => _invoice.AddPosition(null);
		action.Should().ThrowExactly<ArgumentNullException>().WithMessage("*position*");
	}

	[Test]
	public void AddPosition_WhenPositionIsUnavailable_ShouldThrowException()
	{
		Action action = () => _invoice.AddPosition(new InvoicePosition { IsAvailable = false });
		action.Should().Throw<Exception>().WithMessage("*position unavailable*");

		//action.Should().NotThrow();
		//action.Should().NotThrow<Exception>();
	}
}