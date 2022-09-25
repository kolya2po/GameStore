using System.Threading.Tasks;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IGenresService : ICrud<GenreModel>
    {
        Task AddGenreToGameAsync(GameModel gameModel, GenreModel genreModel);
        Task RemoveGenreFromGameAsync(GameModel gameModel, GenreModel genreModel);
    }
}
