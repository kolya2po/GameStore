using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DAL.Entities.Order;

namespace GameStore.DAL.Interfaces
{
    public interface IPaymentTypeRepository
    {
        Task<IEnumerable<PaymentType>> GetAllAsync();
    }
}
