using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.Models;
using Microsoft.AspNetCore.Http;

namespace GameStore.BLL.Interfaces
{
    public interface IGamesService : ICrud<GameModel>
    {
        Task AddImageAsync(int gameId, IFormFile image, HttpRequest request);

        Task<IEnumerable<GameModel>> GetGamesByFilterAsync(FilterSearchModel filterModel);
    }
}
