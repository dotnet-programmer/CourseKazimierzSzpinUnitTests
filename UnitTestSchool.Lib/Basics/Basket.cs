namespace UnitTestSchool.Lib.Basics;

public class Basket
{
	public decimal TotalPrice { get; set; }

	public List<Product> Products { get; } = [];

	public void AddProduct(Product product)
	{
		TotalPrice += product.Price;
		Products.Add(product);
	}
}