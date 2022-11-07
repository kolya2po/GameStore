using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebApi.Models.Cart;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartModel>> GetCart(int id)
        {
            if (id == 0)
            {
                var userName = User.FindFirst("user-name")?.Value;

                var cartModel = await _cartsService.CreateAsync(userName);
                return Ok(cartModel);
            }
            
            var cart = await _cartsService.GetByIdAsync(id);
            return Ok(cart);
        }

        [HttpPost("{id:int}/game/{gameId:int}")]
        public async Task<ActionResult<CartItemModel>> AddGame(int id, int gameId)
        {
            var game = await _gamesService.GetByIdAsync(gameId);

            if (game == null)
            {
                return NotFound("Game doesn't exist.");
            }

            if (id != 0)
            {
                await _cartsService.AddGameAsync(id, game);
                return Ok();
            }

            var userName = User.FindFirst("user-name")?.Value;

            var cartModel = await _cartsService.CreateAsync(userName);
            await _cartsService.AddGameAsync(cartModel.Id, game);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCartDto model)
        {
            var cartModel = Mapper.Map<CartModel>(model);
            await _cartsService.UpdateAsync(cartModel);
            return Ok();
        }

        [HttpDelete("{id:int}/item/{gameId:int}")]
        public async Task<ActionResult> RemoveCartItem(int id, int gameId)
        {
            await _cartsService.RemoveItemAsync(id, gameId);
            return NoContent();
        }
    }
}
