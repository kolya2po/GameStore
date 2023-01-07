using System.Linq;
using System.Threading.Tasks;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class CommentsRepository : BaseRepository, ICommentsRepository
    {
        public CommentsRepository(GameStoreDbContext dbContext) : base(dbContext) { }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var comment = await DbContext.Comments
                .AsNoTracking()
                .Include(c => c.Replies)
                .ThenInclude(c => c.Replies)
                .FirstOrDefaultAsync(c => c.Id == id);

            return comment;
        }

        public async Task CreateAsync(Comment comment)
        {
            await DbContext.Comments.AddAsync(comment);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var comment = await GetByIdAsync(id);

            if (comment == null)
            {
               return;
            }

            if (comment.Replies != null && comment.Replies.Any())
            {
                foreach (var reply in comment.Replies)
                {
                    if (reply.Replies != null && reply.Replies.Any())
                    {
                        foreach (var replyToReply in reply.Replies)
                        {
                            DbContext.Comments.Remove(replyToReply);
                        }
                    }
                    DbContext.Comments.Remove(reply);
                }
            }
            DbContext.Comments.Remove(comment);
        }

        public void Update(Comment comment)
        {
            DbContext.Comments.Update(comment);
        }
    }
}
