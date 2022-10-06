using System.Threading.Tasks;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class CartsRepository : BaseRepository, ICartsRepository
    {
        public CartsRepository(GameStoreDbContext dbContext) : base(dbContext) { }

        public async Task<Cart> GetByIdWithDetailsAsync(int id)
        {
            return await DbContext.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateAsync(Cart cart)
        {
            await DbContext.Carts.AddAsync(cart);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var cart = await DbContext.Carts.FirstOrDefaultAsync(c => c.Id == id);

            if (cart != null)
            {
                DbContext.Carts.Remove(cart);
            }
        }
    }
}
