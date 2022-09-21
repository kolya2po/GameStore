using System.Threading.Tasks;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentsService
    {
        Task<CommentModel> GetByIdAsync(int id);
        Task<CommentModel> ReplyToCommentAsync(CommentModel parentComment,
            CommentModel reply);
        Task<CommentModel> CreateCommentForGameAsync(GameModel gameModel, CommentModel commentModel);
        Task UpdateAsync(CommentModel model);
        Task DeleteAsync(int modelId);
    }
}
