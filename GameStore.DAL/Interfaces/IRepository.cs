using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task CreateAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteByIdAsync(int id);
        void Update(TEntity entity);
    }
}
