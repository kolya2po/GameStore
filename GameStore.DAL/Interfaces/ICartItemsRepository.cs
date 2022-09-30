using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface ICartItemsRepository
    {
        Task CreateAsync(CartItem item);
        Task DeleteByIdAsync(int id);
    }
}
