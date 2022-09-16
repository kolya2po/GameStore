using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class GameGenresRepository : BaseRepository, IGameGenresRepository
    {
        public GameGenresRepository(GameStoreDbContext dbContext) : base(dbContext) { }

        public async Task<GameGenre> GetByIdAsync(int gameId, int genreId)
        {
            var gameGenre = await DbContext.GameGenres.FirstOrDefaultAsync(c => c.GameId == gameId && c.GenreId == genreId);

            return gameGenre;
        }

        public async Task<IEnumerable<GameGenre>> GetAllAsync()
        {
            return await DbContext.GameGenres.ToListAsync();
        }

        public async Task CreateAsync(GameGenre entity)
        {
            await DbContext.GameGenres.AddAsync(entity);
        }

        public void Delete(GameGenre entity)
        {
            DbContext.GameGenres.Remove(entity);
        }

        public async Task DeleteByIdAsync(int gameId, int genreId)
        {
            var gameGenre = await DbContext.GameGenres.FirstOrDefaultAsync(c => c.GameId == gameId && c.GenreId == genreId);

            if (gameGenre != null)
            {
                DbContext.GameGenres.Remove(gameGenre);
            }
        }

        public void Update(GameGenre entity)
        {
            DbContext.GameGenres.Update(entity);
        }

        public async Task<IEnumerable<GameGenre>> GetAllWithDetailsAsync()
        {
            return await DbContext.GameGenres
                .Include(c => c.Game)
                .Include(c => c.Genre)
                .ToListAsync();
        }

        public async Task<GameGenre> GetByIdWithDetailsAsync(int gameId, int genreId)
        {
            return await DbContext.GameGenres
                .Include(c => c.Game)
                .Include(c => c.Genre)
                .FirstOrDefaultAsync(c => c.GameId == gameId && c.GenreId == genreId);
        }
    }
}
