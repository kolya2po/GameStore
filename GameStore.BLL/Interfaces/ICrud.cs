using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface ICrud<TModel>
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(int id);
        Task<TModel> CreateAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(int modelId);
    }
}
