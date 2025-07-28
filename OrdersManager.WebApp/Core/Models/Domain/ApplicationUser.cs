using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace OrdersManager.WebApp.Core.Models.Domain;

public class ApplicationUser : IdentityUser
{
	public bool IsNewUser { get; set; }

	public ICollection<Order> Orders { get; set; } = new Collection<Order>();
}