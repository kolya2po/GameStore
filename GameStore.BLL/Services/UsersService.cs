using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Infrastructure.Exceptions;
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

        public UsersService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, JwtHandler jwtHandler)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new UserNotFoundException($"User with id {id} doesn't exist.");
            }

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
            var token = GetJwtToken();

            return new UserDto {UserId = user.Id, Token = token};
        }

        public async Task<UserDto> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                throw new UserNotFoundException($"User with username {model.UserName} doesn't exist.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsPersistent, false);

            if (!result.Succeeded)
            {
                throw new GameStoreException("Incorrect user's credentials.");
            }

            var token = GetJwtToken();

            return new UserDto { UserId = user.Id, Token = token };
        }

        public async Task AddAvatarAsync(int userId, IFormFile avatar, HttpRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            ValidateParameters(user, userId, avatar);

            const string pathToFolder = @"D:\Items\Avatars";
            var format = avatar.FileName.Split('.')[^1];
            var imageName = $"{Guid.NewGuid()}.{format}";

            var path = Path.Combine(pathToFolder, imageName);

            await using var stream = new FileStream(path, FileMode.Create);
            await avatar.CopyToAsync(stream);

            var domainName = $"{request.Scheme}://{request.Host.Value}";

            user.AvatarImagePath = $"{domainName}/Avatars/{imageName}";
            await _userManager.UpdateAsync(user);

        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        private string GetJwtToken()
        {
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var configuredToken = _jwtHandler.GenerateToken(signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(configuredToken);

            return token;
        }

        private static void ValidateParameters(User user, int userId, IFormFile avatar)
        {
            if (user == null)
            {
                throw new UserNotFoundException($"User with id {userId} doesn't exist.");
            }

            if (avatar == null)
            {
                throw new GameStoreException("Avatar was null.");
            }

            if (!avatar.ContentType.Contains("image"))
            {
                throw new InvalidFileContentTypeException();
            }
        }
    }
}
