using System.Threading.Tasks;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface ICartsService
    {
        Task<CartModel> GetByIdAsync(int id);
        Task<CartModel> GetByUserNameAsync(string userName);
        Task<CartModel> CreateAsync(string userName);
        Task AddGameAsync(int cartId, GameModel game);
        Task RemoveItemAsync(int cartId, int gameId);
        Task UpdateAsync(CartModel cartModel);
        Task DeleteByIdAsync(int id);
    }
}
