using System.Threading.Tasks;
using GameStore.DAL.Entities.Order;

namespace GameStore.DAL.Interfaces
{
    public interface IOrdersRepository
    {
        Task CreateAsync(Order order);
        Task<Order> GetByIdWithDetailsAsync(int id);
        void Update(Order order);
    }
}
