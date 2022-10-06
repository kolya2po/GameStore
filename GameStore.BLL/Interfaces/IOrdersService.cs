using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IOrdersService
    {
        Task<OrderModel> CreateAsync(IEnumerable<CartItemModel> cartItems);
        Task ConfirmOrder(OrderModel orderModel);
        Task UpdateOrderItems(OrderModel orderModel, IEnumerable<CartItemModel> cartItems);
        Task<OrderModel> GetByIdAsync(int id);
    }
}
