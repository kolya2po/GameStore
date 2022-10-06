using System.Threading.Tasks;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface ICartsService
    {
        Task<CartModel> GetByIdAsync(int id);
        Task<CartModel> CreateAsync();
        Task AddGameAsync(int cartId, GameModel game);
        Task DeleteByIdAsync(int id);
    }
}
