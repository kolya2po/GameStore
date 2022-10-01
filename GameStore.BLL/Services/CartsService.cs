using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class CartsService : BaseService, ICartsService
    {
        private readonly ICartItemsService _cartItemsService;

        public CartsService(IUnitOfWork unitOfWork, IMapper mapper, ICartItemsService cartItemsService) : base(unitOfWork, mapper)
        {
            _cartItemsService = cartItemsService;
        }

        public async Task<CartModel> GetByIdAsync(int id)
        {
            var cart = await UnitOfWork.CartsRepository.GetByIdWithDetailsAsync(id);

            return Mapper.Map<CartModel>(cart);
        }

        public async Task<CartModel> CreateAsync(CartModel model)
        {
            var cart = Mapper.Map<Cart>(model);

            await UnitOfWork.CartsRepository.CreateAsync(cart);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<CartModel>(cart);
        }

        public async Task AddGameAsync(int cartId, GameModel game)
        {
            await ValidateCartAsync(cartId);

            var cartItemModel = await _cartItemsService.GetCartItemByIdAsync(cartId, game.Id);

            if (cartItemModel == null)
            {
                await _cartItemsService.CreateAsync(cartId, game);
                return;
            }

            await _cartItemsService.IncreaseQuantityAsync(cartId, game.Id);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(int cartId, int gameId)
        {
            await _cartItemsService.DeleteByIdAsync(cartId, gameId);
        }

        public async Task DecreaseQuantityAsync(int cartId, GameModel game)
        {
            await ValidateCartAsync(cartId);
            var cartItemModel = await _cartItemsService.GetCartItemByIdAsync(cartId, game.Id);

            if (cartItemModel.Quantity > 1)
            {
                await _cartItemsService.DecreaseQuantityAsync(cartId, game.Id);
                return;
            }

            await _cartItemsService.DeleteByIdAsync(cartId, game.Id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await UnitOfWork.CartsRepository.DeleteByIdAsync(id);
            await UnitOfWork.SaveChangesAsync();
        }

        private async Task ValidateCartAsync(int cartId)
        {
            var cartModel = await GetByIdAsync(cartId);

            if (cartModel == null)
            {
                throw new GameStoreException("Cart doesn't exist.");
            }
        }
    }
}
