using System.Threading.Tasks;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Repositories
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        private readonly UserManager<User> _userManager;
        public UsersRepository(GameStoreDbContext dbContext, UserManager<User> userManager) : base(dbContext)
        {
            _userManager = userManager;
        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            return await DbContext.Users.AsNoTracking()
                .Include(c => c.CreatedComments)
                .Include(c => c.CreatedGames)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(User user)
        {
            DbContext.Users.Update(user);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetByNameAsync(string userName)
        {

            return await DbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
