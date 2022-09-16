using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class GenresRepository : BaseRepository, IGenresRepository
    {
        public GenresRepository(GameStoreDbContext dbContext) : base(dbContext) { }

        public async Task<Genre> GetByIdAsync(int id)
        {
            var genre = await DbContext.Genres.FirstOrDefaultAsync(c => c.Id == id);

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await DbContext.Genres.ToListAsync();
        }

        public async Task CreateAsync(Genre entity)
        {
            await DbContext.Genres.AddAsync(entity);
        }

        public void Delete(Genre entity)
        {
            DbContext.Genres.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var genre = await DbContext.Genres.FirstOrDefaultAsync(c => c.Id == id);

            if (genre != null)
            {
                DbContext.Genres.Remove(genre);
            }
        }

        public void Update(Genre entity)
        {
            DbContext.Genres.Update(entity);
        }

        public async Task<Genre> GetByIdWithDetailsAsync(int id)
        {
            return await DbContext.Genres.Include(c => c.SubGenres)
                .ThenInclude(c => c.Games)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Genre>> GetAllWithDetailsAsync()
        {
            return await DbContext.Genres.Include(c => c.SubGenres)
                .ThenInclude(c => c.Games)
                .ToListAsync();
        }
    }
}
