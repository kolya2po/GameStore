using AutoMapper;
using GameStore.BLL;
using GameStore.DAL;
using GameStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Tests
{
    public static class UnitTestHelper
    {
        public static IMapper CreateMapperFromProfile()
        {
            var autoMapperProfile = new AutoMapperProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperProfile));

            return new Mapper(config);
        }

        public static DbContextOptions<GameStoreDbContext> GetDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<GameStoreDbContext>()
                .UseInMemoryDatabase("TestDb")
                .EnableSensitiveDataLogging()
                .Options;

            using var context = new GameStoreDbContext(options);

            SeedData(context);

            return options;
        }

        private static void SeedData(GameStoreDbContext dbContext)
        {
            dbContext.Games.AddRange(GetTestGames());

            dbContext.SaveChanges();
        }

        public static IEnumerable<Game> GetTestGames() =>
            new[]
            {
                new Game
                {
                    Id = 1,
                    Name = "game 1",
                    Description = "desc 1",
                    Price = 230.33m,
                    AuthorId = 1
                },
                new Game
                {
                    Id = 2,
                    Name = "game 2",
                    Description = "desc 2",
                    Price = 20.31m,
                    AuthorId = 2
                },
                new Game
                {
                    Id = 3,
                    Name = "game 3",
                    Description = "desc 3",
                    Price = 10.13m,
                    AuthorId = 3
                }
            };
    }
}
