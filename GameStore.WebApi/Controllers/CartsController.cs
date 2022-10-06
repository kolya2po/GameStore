using System;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<CartModel>> GetById(int id)
        {
            var cart = await _cartsService.GetByIdAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost("game/{gameId:int}")]
        public async Task<ActionResult> AddGame(int gameId)
        {
            var game = await _gamesService.GetByIdAsync(gameId);

            if (game == null)
            {
                return NotFound("Game doesn't exist.");
            }

            if (Request.Cookies.TryGetValue("cartId", out var cartId))
            {
                await _cartsService.AddGameAsync(Convert.ToInt32(cartId), game);
                return Ok();
            }

            var cartModel = await _cartsService.CreateAsync();
            await _cartsService.AddGameAsync(cartModel.Id, game);
            Response.Cookies.Append("cartId", cartModel.Id.ToString());

            return Ok();
        }
    }
}
