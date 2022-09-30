using GameStore.DAL.Interfaces;
using System.Threading.Tasks;
using GameStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class CartItemsRepository : BaseRepository, ICartItemsRepository
    {
        public CartItemsRepository(GameStoreDbContext dbContext) : base(dbContext) { }

        public async Task CreateAsync(CartItem item)
        {
            await DbContext.CartItems.AddAsync(item);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var item = await DbContext.CartItems.FirstOrDefaultAsync(c => c.Id == id);

            if (item != null)
            {
                DbContext.CartItems.Remove(item);
            }
        }
    }
}
