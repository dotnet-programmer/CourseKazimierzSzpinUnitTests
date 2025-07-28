using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrdersManager.WebApp.Core;
using OrdersManager.WebApp.Core.Models.Domain;

namespace OrdersManager.WebApp.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
	public DbSet<Order> Orders { get; set; }
	public DbSet<Product> Products { get; set; }
}