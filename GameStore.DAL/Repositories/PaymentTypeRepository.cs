using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DAL.Entities.Order;
using GameStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class PaymentTypeRepository : BaseRepository, IPaymentTypeRepository
    {
        public PaymentTypeRepository(GameStoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<PaymentType>> GetAllAsync()
        {
            return await DbContext.PaymentTypes.ToListAsync();
        }
    }
}
