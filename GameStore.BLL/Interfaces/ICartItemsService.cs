using System.Threading.Tasks;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface ICartItemsService
    {
        Task CreateAsync(int cartId, GameModel game);
        Task DeleteByIdAsync(int cartId, int gameId);
        Task<CartItemModel> GetCartItemByIdAsync(int cartId, int gameId);
        Task IncreaseQuantityAsync(int cartId, int gameId);
        Task DecreaseQuantityAsync(int cartId, int gameId);
    }
}
