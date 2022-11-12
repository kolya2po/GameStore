using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities.Order;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrdersService _ordersService;
        private readonly ICartsService _cartsService;

        public OrdersController(IMapper mapper, IOrdersService ordersService, ICartsService cartsService) : base(mapper)
        {
            _ordersService = ordersService;
            _cartsService = cartsService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderModel>> GetById(int id)
        {
            var order = await _ordersService.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("{orderId:int}/cart/{cartId:int}")]
        public async Task<ActionResult<OrderModel>> Create(int orderId, int cartId)
        {
            var cart = await _cartsService.GetByIdAsync(cartId);

            if (cart == null)
            {
                return NotFound("Cart doesn't exist.");
            }

            if (orderId != 0)
            {
                var orderModel = await _ordersService.UpdateOrderItems(orderId, cart.CartItems);

                return Ok(orderModel);
            }

            var order = await _ordersService.CreateAsync(cart.CartItems);

            return Ok(order);
        }

        [HttpPut("{cartId:int}")]
        public async Task<ActionResult> ConfirmOrder(int cartId, OrderModel orderModel)
        {
            var order = await _ordersService.GetByIdAsync(orderModel.Id);

            if (order == null)
            {
                return NotFound("Order doesn't exist.");
            }

            await _ordersService.ConfirmOrder(orderModel);

            await _cartsService.DeleteByIdAsync(cartId);

            return Ok();
        }

        [HttpGet("payment-types")]
        public async Task<ActionResult<IEnumerable<PaymentType>>> GetPaymentTypes()
        {
            return Ok(await _ordersService.GetPaymentTypesAsync());
        }
    }
}
