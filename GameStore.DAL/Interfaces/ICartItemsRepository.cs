using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface ICartItemsRepository
    {
        Task<CartItem> GetByIdAsync(int id);
        Task CreateAsync(CartItem item);
        Task DeleteByIdAsync(int id);
    }
}
