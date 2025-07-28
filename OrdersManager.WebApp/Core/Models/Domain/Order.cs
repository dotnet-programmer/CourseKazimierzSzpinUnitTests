using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OrdersManager.WebApp.Core.Models.Domain;

public class Order
{
	public int Id { get; set; }
	public decimal TotalPrice { get; set; }
	[Required]
	public string FullNumber { get; set; } = default!;
	[Required]
	public string UserId { get; set; } = default!;

	public ApplicationUser? User { get; set; }
	public ICollection<Product> Products { get; set; } = new Collection<Product>();
}