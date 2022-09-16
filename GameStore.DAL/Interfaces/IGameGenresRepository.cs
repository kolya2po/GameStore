using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface IGameGenresRepository
    {
        Task<GameGenre> GetByIdAsync(int gameId, int genreId);
        Task<IEnumerable<GameGenre>> GetAllAsync();
        Task CreateAsync(GameGenre entity);
        void Delete(GameGenre entity);
        Task DeleteByIdAsync(int gameId, int genreId);
        void Update(GameGenre entity);

        Task<IEnumerable<GameGenre>> GetAllWithDetailsAsync();
        Task<GameGenre> GetByIdWithDetailsAsync(int gameId, int genreId);
    }
}
