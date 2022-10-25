using System.Threading.Tasks;
using GameStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameStore.DAL.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetByIdWithDetailsAsync(int id);

        void Update(User user);

        Task<IdentityResult> CreateAsync(User user, string password);
        Task<User> GetByNameAsync(string userName);

    }
}
