namespace UnitTestSchool.Lib.Basics;

// dla tej klasy można napisać 3 testy, sprawdzające czy
// - dodawana pozycja jest nullem
// - właściwość IsAvailable jest false
// - pozycja jest poprawnie dodana do listy pozycji faktury
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