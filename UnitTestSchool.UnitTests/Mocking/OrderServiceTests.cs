namespace UnitTestSchool.UnitTests.Mocking;

internal class OrderServiceTests
{
	[Test]
	public void CalculateDiscount_WhenCustomerIsNew_ShouldReturn0()
	{
		OrderService orderService = new();
		var result = orderService.CalculateDiscount(1m, new Customer { IsNewCustomer = true });
		result.Should().Be(0);
	}

	// to jest zły przypadek, bo nie ma zewnętrznych zależności więc użycie mocka jest bez sensu
	// specjalnie na potrzeby testu został stworzony interfejs ICustomer
	[Test]
	public void CalculateDiscount_WhenCustomerIsNew_ShouldReturn0_Mock()
	{
		Mock<ICustomer> mockCustomer = new();
		mockCustomer
			.Setup(x => x.IsNewCustomer)
			.Returns(true);
		OrderService orderService = new();
		var result = orderService.CalculateDiscountMoq(1m, mockCustomer.Object);
		result.Should().Be(0);
	}
}