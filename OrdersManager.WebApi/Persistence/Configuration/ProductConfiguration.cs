using OrdersManager.Core.Models.Domain;
using System.Data.Entity.ModelConfiguration;

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