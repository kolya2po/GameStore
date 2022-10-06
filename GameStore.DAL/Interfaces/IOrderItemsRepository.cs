using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DAL.Entities.Order;

namespace GameStore.DAL.Interfaces
{
    public interface IOrderItemsRepository
    {
        void CreateRange(IEnumerable<OrderItem> orderItems);
        Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(int orderId);
        void UpdateRange(IEnumerable<OrderItem> orderItems);
        void DeleteRange(IEnumerable<OrderItem> orderItems);
    }
}
