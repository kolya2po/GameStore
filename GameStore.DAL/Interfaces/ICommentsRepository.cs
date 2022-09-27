using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface ICommentsRepository
    {
        Task<Comment> GetByIdAsync(int id);
        Task CreateAsync(Comment comment);
        Task DeleteByIdAsync(int id);
        void Update(Comment comment);
    }
}
