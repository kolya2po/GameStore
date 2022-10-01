using GameStore.BLL.Models;
using System.Threading.Tasks;

namespace GameStore.BLL.Infrastructure
{
    public interface ICartItemsService
    {
        Task CreateAsync(int cartId, GameModel game);
        Task<CartItemModel> GetCartItemByIdAsync(int cartId, int gameId);
        Task IncreaseQuantityAsync(int cartId, int gameId);
    }
}
