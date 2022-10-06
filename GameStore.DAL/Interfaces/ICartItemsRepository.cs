using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface ICartItemsRepository
    {
        Task<CartItem> GetByIdAsync(int cartId, int gameId);
        Task CreateAsync(CartItem item);
        Task DeleteByIdAsync(int cartId, int gameId);
    }
}
