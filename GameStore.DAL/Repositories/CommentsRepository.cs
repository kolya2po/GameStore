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
            var comment = await DbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            return comment;
        }

        public async Task CreateAsync(Comment comment)
        {
            await DbContext.Comments.AddAsync(comment);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var comment = await DbContext.Comments
                .Include(c => c.Replies)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment != null)
            {
                if (comment.Replies != null && comment.Replies.Any())
                {
                    foreach (var reply in comment.Replies)
                    {
                        DbContext.Comments.Remove(reply);
                    }
                }
                DbContext.Comments.Remove(comment);
            }
        }

        public void Update(Comment comment)
        {
            DbContext.Comments.Update(comment);
        }
    }
}
