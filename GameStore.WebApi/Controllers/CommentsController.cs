using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebApi.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly IGamesService _gamesService;
        private readonly ICommentsService _commentsService;
        public CommentsController(IMapper mapper, IGamesService gamesService, ICommentsService commentsService) : base(mapper)
        {
            _gamesService = gamesService;
            _commentsService = commentsService;
        }

        [HttpPost]
        public async Task<ActionResult<CommentModel>> CreateCommentForGame(CreateCommentDto commentDto)
        {
            var gameModel = await _gamesService.GetByIdAsync(commentDto.GameId);

            if (gameModel == null)
            {
                return NotFound("Game doesn't exist.");
            }

            var commentModel = Mapper.Map<CommentModel>(commentDto);

            return Ok(await _commentsService.CreateCommentForGameAsync(gameModel, commentModel));
        }

        
        [HttpPost("{commentId:int}")]
        public async Task<ActionResult<CommentModel>> CreateReply(int commentId, CreateCommentDto commentDto)
        {
            var commentModel = await _commentsService.GetByIdAsync(commentId);

            if (commentModel == null)
            {
                return NotFound("Comment doesn't exist.");
            }

            var replyModel = Mapper.Map<CommentModel>(commentDto);

            return Ok(await _commentsService.ReplyToCommentAsync(commentModel, replyModel));
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCommentDto commentDto)
        {
            var comment = await _commentsService.GetByIdAsync(commentDto.Id);

            if (comment == null)
            {
                return NotFound();
            }

            var commentModel = Mapper.Map<CommentModel>(commentDto);
            await _commentsService.UpdateAsync(commentModel);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _commentsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
