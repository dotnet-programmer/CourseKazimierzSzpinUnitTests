namespace UnitTestSchool.Lib.Basics;

public class Offer
{
	public string Title { get; private set; }

	public event EventHandler<Guid> OfferChanged;

	public void SetTitle(string title)
	{
		if (string.IsNullOrWhiteSpace(title))
		{
			throw new ArgumentNullException(nameof(title));
		}

		Title = title;

		OfferChanged?.Invoke(this, Guid.NewGuid());
	}
}