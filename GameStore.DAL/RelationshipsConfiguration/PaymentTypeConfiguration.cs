using GameStore.DAL.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.RelationshipsConfiguration
{
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasMany<Order>()
                .WithOne(c => c.PaymentType)
                .HasForeignKey(c => c.PaymentTypeId);

            builder.HasData(
                new PaymentType
                {
                    Id = 1,
                    Name = "Card"
                },
                new PaymentType
                {
                    Id = 2,
                    Name = "Cash"
                });
        }
    }
}
