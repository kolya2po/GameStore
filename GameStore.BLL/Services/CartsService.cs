using System.Linq;
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
        public CartsService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

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
            var cartModel = await GetByIdAsync(cartId);

            if (cartModel == null)
            {
                throw new GameStoreException("Cart doesn't exist.");
            }

            var cartItem = cartModel.CartItems.FirstOrDefault(c =>
                c.GameId == game.Id);

            if (cartItem == null)
            {
                var newItem = new CartItem
                {
                    CartId = cartModel.Id,
                    GameId = game.Id,
                    Quantity = 1
                };

                await UnitOfWork.CartItemsRepository.CreateAsync(newItem);
                await UnitOfWork.SaveChangesAsync();
                return;
            }

            cartItem.Quantity++;
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task RemoveGameAsync(int cartId, GameModel game)
        {
            var cartModel = await GetByIdAsync(cartId);

            if (cartModel == null)
            {
                throw new GameStoreException("Cart doesn't exist.");
            }

            var cartItem = cartModel.CartItems.FirstOrDefault(c => c.GameId == game.Id);

            if (cartItem == null)
            {
                throw new GameStoreException("Game is not in cart.");
            }

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                await UnitOfWork.SaveChangesAsync();
                return;
            }

            await UnitOfWork.CartItemsRepository.DeleteByIdAsync(cartItem.Id);
            await UnitOfWork.SaveChangesAsync();
        }


        public async Task DeleteByIdAsync(int id)
        {
            await UnitOfWork.CartsRepository.DeleteByIdAsync(id);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
