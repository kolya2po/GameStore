using GameStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DAL.RelationshipsConfiguration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasOne<Genre>()
                .WithMany(c => c.SubGenres)
                .HasForeignKey(c => c.ParentGenreId);

            builder.HasData(new[]
            {
                new Genre
                {
                    Id = 1,
                    Name = "Races"
                },
                new Genre
                {
                    Id = 2,
                    ParentGenreId = 1,
                    Name = "Rally"
                },
                new Genre
                {
                    Id = 3,
                    ParentGenreId = 1,
                    Name = "Arcade"
                },
                new Genre
                {
                    Id = 4,
                    ParentGenreId = 1,
                    Name = "Formula"
                },
                new Genre
                {
                    Id = 5,
                    ParentGenreId = 1,
                    Name = "Off-road"
                },
                new Genre
                {
                    Id = 6,
                    Name = "Rpg"
                },
                new Genre
                {
                    Id = 7,
                    Name = "Strategy"
                },
                new Genre
                {
                    Id = 8,
                    Name = "Sports"
                },
                new Genre
                {
                    Id = 9,
                    Name = "Adventure"
                },
                new Genre
                {
                    Id = 10,
                    Name = "Action"
                },
                new Genre
                {
                    Id = 11,
                    ParentGenreId = 10,
                    Name = "Fps"
                },
                new Genre
                {
                    Id = 12,
                    ParentGenreId = 10,
                    Name = "Tps"
                },
                new Genre
                {
                    Id = 13,
                    ParentGenreId = 10,
                    Name = "Misc"
                },
                new Genre
                {
                    Id = 14,
                    Name = "Puzzle & skill"
                },
                new Genre
                {
                    Id = 15,
                    Name = "Other"
                }
            });
        }
    }
}
