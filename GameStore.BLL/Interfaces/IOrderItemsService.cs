using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.Models;
using GameStore.DAL.Entities.Order;

namespace GameStore.BLL.Interfaces
{
    public interface IOrderItemsService
    {
        Task<IEnumerable<OrderItem>> CreateRangeAsync(int orderId, IEnumerable<CartItemModel> cartItems);
        Task UpdateAsync(OrderModel orderModel, IEnumerable<CartItemModel> cartItems);
    }
}
