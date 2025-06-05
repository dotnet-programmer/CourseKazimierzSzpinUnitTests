namespace UnitTestSchool.Lib.Mocking;

public class OrderService
{
	// na podstawie przekazanego klienta oraz kwoty zamówienia zwraca wartość zniżki
	public decimal CalculateDiscount(decimal orderAmount, Customer customer)
	{
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

	// specjalnie na potrzeby testu został stworzony interfejs ICustomer
	public decimal CalculateDiscountMoq(decimal orderAmount, ICustomer customer)
	{
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