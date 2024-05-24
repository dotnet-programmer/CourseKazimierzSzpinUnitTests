using OrdersManager.Models;
using System.Data.Entity.ModelConfiguration;

namespace OrdersManager.Persistence.Configuration
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            HasMany(x => x.Orders)
                .WithRequired(x => x.User)
                .WillCascadeOnDelete(false);
        }
    }
}