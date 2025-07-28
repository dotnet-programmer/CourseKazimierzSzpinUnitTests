using System.ComponentModel.DataAnnotations;

namespace OrdersManager.WebApp.Core.Models.Domain;

public class Product
{
	public int Id { get; set; }
	[Required]
	public string Name { get; set; } = default!;
	public decimal Price { get; set; }
	public decimal Quantity { get; set; }
	public int OrderId { get; set; }

	public Order? Order { get; set; }
}