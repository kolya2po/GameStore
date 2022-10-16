using System.Threading.Tasks;
using GameStore.DAL.Entities.Order;
using GameStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class OrdersRepository : BaseRepository, IOrdersRepository
    {
        public OrdersRepository(GameStoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateAsync(Order order)
        {
            await DbContext.Orders.AddAsync(order);
        }

        public async Task<Order> GetByIdWithDetailsAsync(int id)
        {
            return await DbContext.Orders
                .Include(c => c.ContactInformation)
                .Include(c => c.OrderItems)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(Order order)
        {
            DbContext.Orders.Update(order);
        }
    }
}
