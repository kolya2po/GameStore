using System;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
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

        [HttpPost]
        public async Task<ActionResult<OrderModel>> Create()
        {
            Request.Cookies.TryGetValue("cartId", out var cartId);

            var cart = await _cartsService.GetByIdAsync(Convert.ToInt32(cartId));

            if (cart == null)
            {
                return NotFound("Cart doesn't exist.");
            }

            if (Request.Cookies.TryGetValue("orderId", out var orderId))
            {
                var orderModel = await _ordersService.UpdateOrderItems(Convert.ToInt32(orderId), cart.CartItems);

                return Ok(orderModel);
            }

            var order = await _ordersService.CreateAsync(cart.CartItems);
            Response.Cookies.Append("orderId", order.Id.ToString());

            return Ok(order);
        }

        [HttpPut]
        public async Task<ActionResult> ConfirmOrder(OrderModel model)
        {
            var order = await _ordersService.GetByIdAsync(model.Id);

            if (order == null)
            {
                return NotFound("Order doesn't exist.");
            }

            await _ordersService.ConfirmOrder(order);

            var cartId = Request.Cookies["cartId"];
            await _cartsService.DeleteByIdAsync(Convert.ToInt32(cartId));
            Response.Cookies.Delete("cartId");

            return Ok();
        }
    }
}
