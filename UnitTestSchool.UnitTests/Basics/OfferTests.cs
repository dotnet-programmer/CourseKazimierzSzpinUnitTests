namespace UnitTestSchool.UnitTests.Basics;

internal class OfferTests
{
	private Offer _offer;

	[SetUp]
	public void SetUp()
		=> _offer = new();

	// testowanie zdarzeń
	[Test]
	public void SetTitle_WhenArgumentIsValid_ShouldRaiseOfferChangedEvent()
	{
		using (var monitoredSubject = _offer.Monitor())
		{
			_offer.SetTitle("1");
			monitoredSubject.Should().Raise("OfferChanged");
		}
	}

	[TestCase("")]
	[TestCase(" ")]
	//[TestCase(null)]
	public void SetTitle_WhenArgumentIsNullOrWhiteSpace_ShouldThrowArgumentNullException(string title)
	{
		Action action = () => _offer.SetTitle(title);
		action.Should().ThrowExactly<ArgumentNullException>().WithMessage("*title*");
	}

	[Test]
	public void SetTitle_WhenArgumentIsValid_ShouldChangeTitle()
	{
		_offer.SetTitle("1");
		_offer.Title.Should().Be("1");
	}
}