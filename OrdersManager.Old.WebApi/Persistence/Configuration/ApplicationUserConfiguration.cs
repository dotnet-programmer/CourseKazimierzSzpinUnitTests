using System.Data.Entity.ModelConfiguration;
using OrdersManager.Models;

namespace OrdersManager.Persistence.Configuration
{
	public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
	{
		public ApplicationUserConfiguration()
			=> HasMany(x => x.Orders)
				.WithRequired(x => x.User)
				.WillCascadeOnDelete(false);
	}
}