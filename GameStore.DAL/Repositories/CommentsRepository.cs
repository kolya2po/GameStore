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
            var comment = await DbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment != null)
            {
                DbContext.Comments.Remove(comment);
            }
        }

        public void Update(Comment entity)
        {
            DbContext.Comments.Update(entity);
        }
    }
}
