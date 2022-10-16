using System.Collections.Generic;
using GameStore.DAL.Entities.Order;

namespace GameStore.DAL.Interfaces
{
    public interface IOrderItemsRepository
    {
        void CreateRange(IEnumerable<OrderItem> orderItems);
        void DeleteRange(IEnumerable<OrderItem> orderItems);
    }
}
