using System.Collections.Generic;
using GameStore.DAL.Entities.Order;
using GameStore.DAL.Interfaces;

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

        public void DeleteRange(IEnumerable<OrderItem> orderItems)
        {
            DbContext.OrderItems.RemoveRange(orderItems);
        }
    }
}
