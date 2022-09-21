using System.Threading.Tasks;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace GameStore.BLL.Interfaces
{
    public interface IGamesService : ICrud<GameModel>
    {
        Task AddImageAsync(Game game, IFormFile image, HttpRequest request);
    }
}
