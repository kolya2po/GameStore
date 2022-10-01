using GameStore.DAL.Interfaces;
using System.Threading.Tasks;
using GameStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class CartItemsRepository : BaseRepository, ICartItemsRepository
    {
        public CartItemsRepository(GameStoreDbContext dbContext) : base(dbContext) { }

        public async Task<CartItem> GetByIdAsync(int cartId, int gameId)
        {
            return await DbContext.CartItems
                .FirstOrDefaultAsync(c => c.CartId == cartId && c.GameId == gameId);
        }

        public async Task CreateAsync(CartItem item)
        {
            await DbContext.CartItems.AddAsync(item);
        }

        public async Task DeleteByIdAsync(int cartId, int gameId)
        {
            var item = await DbContext.CartItems
                .FirstOrDefaultAsync(c => c.CartId == cartId && c.GameId == gameId);

            if (item != null)
            {
                DbContext.CartItems.Remove(item);
            }
        }
    }
}
