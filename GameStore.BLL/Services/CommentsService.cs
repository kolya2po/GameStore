using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class CommentsService : BaseService, ICommentsService
    {
        public CommentsService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) {  }

        public async Task<CommentModel> GetByIdAsync(int id)
        {
            var comment = await UnitOfWork.CommentsRepository.GetByIdAsync(id);
            return Mapper.Map<CommentModel>(comment);
        }

        // Should I return created comment from db? 
        // Because I need to have comment with id on the client 
        // to have possibility to make reply.
        
        public async Task<CommentModel> ReplyToCommentAsync(CommentModel parentComment, CommentModel reply)
        {
            var comment = Mapper.Map<Comment>(reply);
            comment.ParentCommentId = parentComment.Id;

            await UnitOfWork.CommentsRepository.CreateAsync(comment);
            await UnitOfWork.SaveChangesAsync();

            return reply;
        }

        // Should I return created comment from db? 
        // Because I need to have comment with id on the client 
        // to have possibility to make reply.
        public async Task<CommentModel> CreateCommentForGameAsync(GameModel gameModel, CommentModel commentModel)
        {
            var comment = Mapper.Map<Comment>(commentModel);
            comment.GameId = gameModel.Id;

            await UnitOfWork.CommentsRepository.CreateAsync(comment);
            await UnitOfWork.SaveChangesAsync();

            return commentModel;
        }

        public async Task UpdateAsync(CommentModel model)
        {
            var comment = Mapper.Map<Comment>(model);

            UnitOfWork.CommentsRepository.Update(comment);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await UnitOfWork.CommentsRepository.DeleteByIdAsync(modelId);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
