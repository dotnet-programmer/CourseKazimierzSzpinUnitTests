using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using OrdersManager.Core;
using OrdersManager.Core.Models.Domain;

namespace OrdersManager.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
	{
		public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public DbSet<Order> Orders { get; set; }
		public DbSet<Product> Products { get; set; }

		public static ApplicationDbContext Create()
			=> new ApplicationDbContext();
	}
}