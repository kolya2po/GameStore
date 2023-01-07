using System.Threading.Tasks;
using GameStore.BLL.Models;
using Microsoft.AspNetCore.Http;

namespace GameStore.BLL.Interfaces
{
    public interface IGamesService : ICrud<GameModel>
    {
        Task<string> AddImageAsync(GameModel gameModel, IFormFile image, HttpRequest request);
    }
}
