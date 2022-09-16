using GameStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.RelationshipsConfiguration
{
    public class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        public void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            builder.HasKey(c => new { c.GameId, c.GenreId });

            builder.HasOne(c => c.Game)
                .WithMany(c => c.Genres)
                .HasForeignKey(c => c.GameId);

            builder.HasOne(c => c.Genre)
                .WithMany(c => c.Games)
                .HasForeignKey(c => c.GenreId);
        }
    }
}
