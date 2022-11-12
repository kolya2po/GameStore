using GameStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.RelationshipsConfiguration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasMany(c => c.CartItems)
                .WithOne()
                .HasForeignKey(c => c.CartId);

            builder.Property(c => c.TotalPrice)
                .HasColumnType("decimal(15, 3)");
        }
    }
}
