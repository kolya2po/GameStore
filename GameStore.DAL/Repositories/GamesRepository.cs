using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class GamesRepository : BaseRepository, IGamesRepository
    {
        public GamesRepository(GameStoreDbContext dbContext) : base(dbContext) { }

        public async Task<Game> GetByIdAsync(int id)
        {
            var game = await DbContext.Games.FirstOrDefaultAsync(c => c.Id == id);

            return game;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await DbContext.Games.ToListAsync();
        }

        public async Task CreateAsync(Game entity)
        {
            await DbContext.Games.AddAsync(entity);
        }

        public void Delete(Game entity)
        {
            DbContext.Games.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DbContext.Database
                .ExecuteSqlInterpolatedAsync($"EXEC deleteGameById @id={id}");
        }

        public void Update(Game entity)
        {
            DbContext.Games.Update(entity);
        }

        public async Task<IEnumerable<Game>> GetAllWithDetailsAsync()
        {
            return await DbContext.Games.Include(c => c.Genres)
                .ThenInclude(c => c.Genre)
                .ThenInclude(c => c.SubGenres)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Game> GetByIdWithDetailsAsync(int id)
        {
            return await DbContext.Games.AsNoTracking()
                .Include(c => c.Genres)
                .ThenInclude(c => c.Genre)
                .Include(c => c.Comments)
                .ThenInclude(c => c.Replies)
                .ThenInclude(r => r.Replies)
                .AsSplitQuery()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
