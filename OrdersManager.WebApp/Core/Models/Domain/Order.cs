using System.Collections.ObjectModel;

namespace OrdersManager.WebApp.Core.Models.Domain;

public class Order
{
	public int Id { get; set; }
	public decimal TotalPrice { get; set; }
	public string FullNumber { get; set; }
	public string UserId { get; set; }

	public ApplicationUser User { get; set; }
	public ICollection<Product> Products { get; set; } = new Collection<Product>();
}