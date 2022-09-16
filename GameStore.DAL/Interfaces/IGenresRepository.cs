using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface IGenresRepository : IRepository<Genre>
    {
        Task<Genre> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Genre>> GetAllWithDetailsAsync();
    }
}
