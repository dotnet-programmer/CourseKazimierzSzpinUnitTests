using System.Data.Entity.ModelConfiguration;
using OrdersManager.Core.Models.Domain;

namespace OrdersManager.Persistence.Configuration
{
	public class ProductConfiguration : EntityTypeConfiguration<Product>
	{
		public ProductConfiguration()
		{
			ToTable("Products");

			HasKey(x => x.Id);

			Property(x => x.Name)
				.IsRequired();
		}
	}
}