using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class CommentsRepository : BaseRepository, ICommentsRepository
    {
        public CommentsRepository(GameStoreDbContext dbContext) : base(dbContext) {  }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var comment = await DbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            return comment;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await DbContext.Comments.ToListAsync();
        }

        public async Task CreateAsync(Comment entity)
        {
            await DbContext.Comments.AddAsync(entity);
        }

        public void Delete(Comment entity)
        {
            DbContext.Comments.Remove(entity);
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

        public async Task<IEnumerable<Comment>> GetAllWithDetailsAsync()
        {
            return await DbContext.Comments
                .Include(c => c.Author)
                .Include(c => c.Replies)
                .ToListAsync();
        }

        public async Task<Comment> GetByIdWithDetailsAsync(int id)
        {
            return await DbContext.Comments
                .Include(c => c.Author)
                .Include(c => c.Replies)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
