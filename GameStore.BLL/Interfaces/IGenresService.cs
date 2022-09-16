using System.Threading.Tasks;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IGenresService : ICrud<GenreModel>
    {
        Task AddGenreToGameAsync(int gameId, int genreId);
        Task RemoveGenreFromGameAsync(int gameId, int genreId);
    }
}
