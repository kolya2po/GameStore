using GameStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.RelationshipsConfiguration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(c => new { c.CartId, c.GameId });

            builder.HasOne(c => c.Game)
                .WithOne()
                .HasForeignKey<CartItem>(c => c.GameId);
        }
    }
}
