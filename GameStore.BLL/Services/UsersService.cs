using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models.Identity;
using GameStore.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GameStore.BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtHandler _jwtHandler;
        private readonly IImagesService _imagesService;

        public UsersService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, JwtHandler jwtHandler, IImagesService imagesService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtHandler = jwtHandler;
            _imagesService = imagesService;
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserDto> RegisterAsync(RegistrationModel model)
        {
            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join("\n", result
                    .Errors.Select(c => c.Description));

                throw new GameStoreException(errors);
            }

            await _signInManager.SignInAsync(user, false);
            var token = _jwtHandler.GetJwtToken(Enumerable.Empty<Claim>());

            return new UserDto {UserId = user.Id, Token = token};
        }

        public async Task<UserDto> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                throw new GameStoreException($"User with username {model.UserName} doesn't exist.");
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsPersistent, false);

            if (!result.Succeeded)
            {
                throw new GameStoreException("Incorrect user's credentials.");
            }

            var token = _jwtHandler.GetJwtToken(Enumerable.Empty<Claim>());

            return new UserDto { UserId = user.Id, Token = token };
        }

        public async Task AddAvatarAsync(User user, IFormFile avatar, HttpRequest request)
        {
            user.AvatarImagePath = await _imagesService.SaveImageAsync(avatar, MediaPathHelper.PathToUsersAvatars, request);
            await _userManager.UpdateAsync(user);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
