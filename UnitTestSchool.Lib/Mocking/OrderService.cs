namespace UnitTestSchool.Lib.Mocking;

public class OrderService
{
	public decimal CalculateDiscount(decimal orderAmount, ICustomer customer)
	{
		if (customer == null)
		{
			throw new ArgumentNullException(nameof(customer));
		}

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