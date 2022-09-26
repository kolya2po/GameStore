using System.Threading.Tasks;
using GameStore.BLL.Models.Identity;
using GameStore.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace GameStore.BLL.Interfaces
{
    public interface IUsersService
    {
        Task<UserModel> GetByIdAsync(int id);
        Task<UserDto> RegisterAsync(RegistrationModel model);
        Task<UserDto> LoginAsync(LoginModel model);
        Task AddAvatarAsync(User user, IFormFile avatar, HttpRequest request);
        Task SignOutAsync();
    }
}
