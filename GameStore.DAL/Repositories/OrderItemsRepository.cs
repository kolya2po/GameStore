using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.DAL.Entities.Order;
using GameStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class OrderItemsRepository : BaseRepository, IOrderItemsRepository
    {
        public OrderItemsRepository(GameStoreDbContext dbContext) : base(dbContext)
        {
        }

        public void CreateRange(IEnumerable<OrderItem> orderItems)
        {
            DbContext.OrderItems.AddRange(orderItems);
        }

        public async Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(int orderId)
        {
            return await DbContext.OrderItems.Where(c => c.OrderId == orderId)
                .ToListAsync();
        }

        public void UpdateRange(IEnumerable<OrderItem> orderItems)
        {
            DbContext.OrderItems.UpdateRange(orderItems);
        }

        public void DeleteRange(IEnumerable<OrderItem> orderItems)
        {
            DbContext.OrderItems.RemoveRange(orderItems);
        }
    }
}
