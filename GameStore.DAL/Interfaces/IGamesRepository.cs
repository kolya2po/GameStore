using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface IGamesRepository : IRepository<Game>
    {
        Task<IEnumerable<Game>> GetAllWithDetailsAsync();
        Task<Game> GetByIdWithDetailsAsync(int id);
    }
}
