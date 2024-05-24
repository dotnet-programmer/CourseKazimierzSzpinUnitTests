using OrdersManager.Core.Models.Domain;
using OrdersManager.Models;
using System.Data.Entity;

namespace OrdersManager.Core
{
    public interface IApplicationDbContext
    {
        IDbSet<ApplicationUser> Users { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
        int SaveChanges();
    }
}