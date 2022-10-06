using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities.Order;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class OrdersService : BaseService, IOrdersService
    {
        private readonly IOrderItemsService _orderItemsService;
        public OrdersService(IUnitOfWork unitOfWork, IMapper mapper, IOrderItemsService orderItemsService) : base(unitOfWork, mapper)
        {
            _orderItemsService = orderItemsService;
        }

        public async Task<OrderModel> CreateAsync(IEnumerable<CartItemModel> cartItems)
        {
            var order = new Order();

            await UnitOfWork.OrdersRepository.CreateAsync(order);
            await UnitOfWork.SaveChangesAsync();

            var orderItems = await _orderItemsService.CreateRangeAsync(order.Id, cartItems);

            order.OrderItems = orderItems;
            return Mapper.Map<OrderModel>(order);
        }

        public async Task ConfirmOrder(OrderModel orderModel)
        {
            var order = Mapper.Map<Order>(orderModel);

            UnitOfWork.OrdersRepository.Update(order);
            await UnitOfWork.SaveChangesAsync();
        }

        // tvar'
        public async Task UpdateOrderItems(OrderModel orderModel, IEnumerable<CartItemModel> cartItems)
        {
            await _orderItemsService.UpdateAsync(orderModel, cartItems);
        }

        public async Task<OrderModel> GetByIdAsync(int id)
        {
            var order = await UnitOfWork.OrdersRepository.GetByIdWithDetailsAsync(id);
            return Mapper.Map<OrderModel>(order);
        }
    }
}
