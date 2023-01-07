using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.Models;
using GameStore.DAL.Entities.Order;

namespace GameStore.BLL.Interfaces
{
    public interface IOrdersService
    {
        Task<OrderModel> CreateAsync(IEnumerable<CartItemModel> cartItems);
        Task ConfirmOrder(OrderModel orderModel);
        Task<OrderModel> UpdateOrderItems(int orderId, IEnumerable<CartItemModel> cartItems);
        Task<OrderModel> GetByIdAsync(int id);
        Task<IEnumerable<PaymentType>> GetPaymentTypesAsync();
    }
}
