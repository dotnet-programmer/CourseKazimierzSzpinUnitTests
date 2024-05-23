namespace UnitTestSchool.Lib.Mocking;

public class OrderService
{
	// na podstawie przekazanego klienta oraz kwoty zamówienia zwraca wartość zniżki
	public decimal CalculateDiscount(decimal orderAmount, Customer customer)
	{
		//if (customer == null)
		//{
		//	throw new ArgumentNullException(nameof(customer));
		//}
		ArgumentNullException.ThrowIfNull(customer, nameof(customer));

		if (customer.IsNewCustomer)
		{
			return 0;
		}

		if (orderAmount > 100)
		{
			return 20;
		}

		return 0;
	}

	public decimal CalculateDiscountMoq(decimal orderAmount, ICustomer customer)
	{
		//if (customer == null)
		//{
		//	throw new ArgumentNullException(nameof(customer));
		//}
		ArgumentNullException.ThrowIfNull(customer, nameof(customer));

		if (customer.IsNewCustomer)
		{
			return 0;
		}

		if (orderAmount > 100)
		{
			return 20;
		}

		return 0;
	}
}