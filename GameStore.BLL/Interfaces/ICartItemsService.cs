using System.Collections.Generic;
using GameStore.BLL.Models;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface ICartItemsService
    {
        Task<CartItemModel> CreateAsync(int cartId, GameModel game);
        Task DeleteByIdAsync(int cartId, int gameId);
        Task<CartItemModel> GetCartItemByIdAsync(int cartId, int gameId);
        Task IncreaseQuantityAsync(int cartId, int gameId);
        Task DeleteRangeAsync(IEnumerable<CartItemModel> itemsToDelete);
    }
}
