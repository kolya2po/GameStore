using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities.Order;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class OrderItemsService : BaseService, IOrderItemsService
    {
        public OrderItemsService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<OrderItem>> CreateRangeAsync(int orderId, IEnumerable<CartItemModel> cartItems)
        {
            var orderItems = Mapper.Map<IEnumerable<OrderItem>>(cartItems).ToArray();

            foreach (var orderItem in orderItems)
            {
                orderItem.OrderId = orderId;
            }

            UnitOfWork.OrderItemsRepository.CreateRange(orderItems);
            await UnitOfWork.SaveChangesAsync();

            return orderItems;
        }

        public async Task UpdateAsync(OrderModel orderModel, IEnumerable<CartItemModel> cartItems)
        {
            UnitOfWork.OrderItemsRepository.DeleteRange(orderModel.OrderItems);
            orderModel.OrderItems = await CreateRangeAsync(orderModel.Id, cartItems);
        }
    }
}
