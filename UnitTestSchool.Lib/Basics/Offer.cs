namespace UnitTestSchool.Lib.Basics;

public class Offer
{
	private readonly Guid _id;

	public string Title { get; private set; }

	public event EventHandler<Guid> OfferChanged;

	public Offer()
		=> _id = Guid.NewGuid();

	public void SetTitle(string title)
	{
		if (string.IsNullOrWhiteSpace(title))
		{
			throw new ArgumentNullException(nameof(title));
		}

		Title = title;
		OnOfferChanged();
	}

	private void OnOfferChanged()
		=> OfferChanged?.Invoke(this, _id);
}