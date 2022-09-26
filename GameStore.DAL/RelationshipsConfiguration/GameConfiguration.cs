using GameStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.RelationshipsConfiguration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(c => c.Price)
                .HasColumnType("decimal(10,4)");

            builder.HasOne<User>()
                .WithMany(c => c.CreatedGames)
                .HasForeignKey(c => c.AuthorId);
        }
    }
}
