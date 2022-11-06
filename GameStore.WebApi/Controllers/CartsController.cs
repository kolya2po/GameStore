using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebApi.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GameStore.WebApi.Controllers
{
    public class CartsController : BaseController
    {
        private readonly ICartsService _cartsService;
        private readonly IGamesService _gamesService;

        public CartsController(IMapper mapper, ICartsService cartsService, IGamesService gamesService) : base(mapper)
        {
            _cartsService = cartsService;
            _gamesService = gamesService;
        }

        [HttpGet("{id:int?}")]
        public async Task<ActionResult<CartModel>> GetCart(int? id)
        {
            if (!id.HasValue)
            {
                var userName = User.FindFirst("user-name")?.Value;

                var cartModel = await _cartsService.CreateAsync(userName);
                return Ok(cartModel);
            }
            
            var cart = await _cartsService.GetByIdAsync(id.Value);
            return Ok(cart);
        }

        [HttpPost("{id:int?}/game/{gameId:int}")]
        public async Task<ActionResult<CartItemModel>> AddGame(int? id, int gameId)
        {
            var game = await _gamesService.GetByIdAsync(gameId);

            if (game == null)
            {
                return NotFound("Game doesn't exist.");
            }

            if (id.HasValue)
            {
                return Ok(await _cartsService.AddGameAsync(id.Value, game));
            }

            var userName = User.FindFirst("user-name")?.Value;

            var cartModel = await _cartsService.CreateAsync(userName);
            
            //Response.Cookies.Append("cartId", cartModel.Id.ToString());

            return Ok(await _cartsService.AddGameAsync(cartModel.Id, game));
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCartDto model)
        {
            var cartModel = Mapper.Map<CartModel>(model);
            await _cartsService.UpdateAsync(cartModel);
            return Ok();
        }

        [HttpDelete("item/{gameId:int}")]
        public async Task<ActionResult> RemoveCartItem(int gameId)
        {
            Request.Cookies.TryGetValue("cartId", out var cartId);
            await _cartsService.RemoveItemAsync(Convert.ToInt32(cartId), gameId);
            return NoContent();
        }
    }
}
