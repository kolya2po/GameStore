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

        public async Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(int orderId)
        {
            return await UnitOfWork.OrderItemsRepository.GetAllByOrderIdAsync(orderId);
        }

        public async Task UpdateAsync(OrderModel orderModel, IEnumerable<CartItemModel> cartItems)
        {
            var itemsFromCart = Mapper.Map<IEnumerable<OrderItem>>(cartItems).ToArray();

            FindAndDeleteObsoleteOrderItems(orderModel.OrderItems, itemsFromCart);

            var newItems = new List<OrderItem>();

            foreach (var item in itemsFromCart)
            {
                item.OrderId = orderModel.Id;

                var existingOrderItem = orderModel.OrderItems.FirstOrDefault(c => c.GameName == item.GameName);

                if (existingOrderItem != null)
                {
                    existingOrderItem.Quantity = item.Quantity;
                    continue;
                }

                newItems.Add(item);
            }

            UnitOfWork.OrderItemsRepository.CreateRange(newItems);
            await UnitOfWork.SaveChangesAsync();
        }

        private void FindAndDeleteObsoleteOrderItems(IEnumerable<OrderItem> itemsFromOrder,
            IEnumerable<OrderItem> itemsFromCart)
        {
            var itemsToDelete = itemsFromOrder.ExceptBy(itemsFromCart.Select(c => c.GameName), c => c.GameName);

            UnitOfWork.OrderItemsRepository.DeleteRange(itemsToDelete);
        }
    }
}
