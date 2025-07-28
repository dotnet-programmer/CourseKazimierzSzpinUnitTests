namespace OrdersManager.WebApp.Core.Models.Domain;

public class Product
{
	public int Id { get; set; }
	public int OrderId { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
	public decimal Quantity { get; set; }

	public Order Order { get; set; }
}