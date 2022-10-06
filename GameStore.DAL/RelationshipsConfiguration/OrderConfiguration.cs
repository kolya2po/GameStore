using GameStore.DAL.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.RelationshipsConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(c => c.OrderItems)
                .WithOne()
                .HasForeignKey(c => c.OrderId);

            builder.HasOne(c => c.ContactInformation)
                .WithOne()
                .HasForeignKey<ContactInformation>(c => c.OrderId);
        }
    }
}
