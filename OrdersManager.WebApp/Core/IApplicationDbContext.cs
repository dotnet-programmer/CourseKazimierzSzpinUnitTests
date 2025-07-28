using Microsoft.EntityFrameworkCore;
using OrdersManager.WebApp.Core.Models.Domain;

namespace OrdersManager.WebApp.Core;

public interface IApplicationDbContext
{
	DbSet<ApplicationUser> Users { get; set; }
	DbSet<Order> Orders { get; set; }
	DbSet<Product> Products { get; set; }

	int SaveChanges();
	void Dispose();
}