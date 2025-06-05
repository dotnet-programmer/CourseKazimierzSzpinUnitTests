namespace UnitTestSchool.UnitTests.Basics;

internal class BasketTests
{
	// testowanie metody void
	// metoda coś robi, więc trzeba przetestować efekt działania tej metody a nie wartość zwracaną
	[Test]
	public void AddProduct_WhenCalled_ShouldUpdateTotalPrice()
	{
		Basket basket = new() { TotalPrice = 0 };
		basket.AddProduct(new Product { Price = 1 });
		basket.TotalPrice.Should().Be(1);
	}

	// dodatkowe testy mogą być np.
	// - czy produkt jest faktycznie w koszyku
	// - co się stanie jak produkt będzie null
}