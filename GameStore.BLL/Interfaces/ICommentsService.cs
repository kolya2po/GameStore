using System.Threading.Tasks;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentsService
    {
        Task<CommentModel> GetByIdAsync(int id);
        Task<CommentModel> CreateCommentAsync(GameModel gameModel, CommentModel commentModel);
        Task UpdateAsync(CommentModel model);
        Task DeleteAsync(int modelId);
    }
}
