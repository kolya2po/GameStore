using System;
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
        
        public async Task<CommentModel> CreateCommentAsync(GameModel gameModel, CommentModel commentModel)
        {
            var comment = Mapper.Map<Comment>(commentModel);
            comment.GameId = gameModel.Id;

            comment.CreationDate = DateTime.UtcNow;

            await UnitOfWork.CommentsRepository.CreateAsync(comment);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<CommentModel>(comment);
        }

        public async Task UpdateAsync(CommentModel model)
        {
            var comment = Mapper.Map<Comment>(model);

            comment.CreationDate = DateTime.UtcNow;

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
