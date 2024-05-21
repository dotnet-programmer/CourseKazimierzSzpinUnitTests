namespace UnitTestSchool.Lib.Basics;

public class Invoice
{
	public IList<InvoicePosition> Positions { get; set; }

	public void AddPosition(InvoicePosition position)
	{
		//if (position == null)
		//{
		//	throw new ArgumentNullException(nameof(position));
		//}
		ArgumentNullException.ThrowIfNull(position, nameof(position));

		if (!position.IsAvailable)
		{
			throw new Exception("Position unavailable");
		}

		Positions.Add(position);
	}
}