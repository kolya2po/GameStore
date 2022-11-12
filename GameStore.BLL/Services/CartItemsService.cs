using System.Collections.Generic;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Threading.Tasks;

namespace GameStore.BLL.Services
{
    public class CartItemsService : BaseService, ICartItemsService
    {
        public CartItemsService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task CreateAsync(int cartId, GameModel game)
        {
            var cartItem = new CartItem
            {
                CartId = cartId,
                GameId = game.Id,
                Quantity = 1
            };

            await UnitOfWork.CartItemsRepository.CreateAsync(cartItem);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<CartItemModel> GetCartItemByIdAsync(int cartId, int gameId)
        {
            var cartItem = await UnitOfWork.CartItemsRepository.GetByIdAsync(cartId, gameId);

            return Mapper.Map<CartItemModel>(cartItem);
        }

        public async Task IncreaseQuantityAsync(int cartId, int gameId)
        {
            var cartItem = await ValidateAndGetCartItemAsync(cartId, gameId);

            cartItem.Quantity++;
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int cartId, int gameId)
        {
            await UnitOfWork.CartItemsRepository.DeleteByIdAsync(cartId, gameId);
            await UnitOfWork.SaveChangesAsync();
        }

        private async Task<CartItem> ValidateAndGetCartItemAsync(int cartId, int gameId)
        {
            var cartItem = await UnitOfWork.CartItemsRepository.GetByIdAsync(cartId, gameId);

            if (cartItem == null)
            {
                throw new GameStoreException("Game is not in cart.");
            }

            return cartItem;
        }
    }
}