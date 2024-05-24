using OrdersManager.Core.Models.Domain;
using System.Data.Entity.ModelConfiguration;

namespace OrdersManager.Persistence.Configuration
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("Orders");

            HasKey(x => x.Id);

            Property(x => x.UserId)
                .IsRequired();

            Property(x => x.FullNumber)
                .IsRequired();

            HasMany(x => x.Products)
                .WithRequired(x => x.Order)
                .WillCascadeOnDelete(false);
        }
    }
}