using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models.Identity;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GameStore.BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtHandler _jwtHandler;
        private readonly IImagesService _imagesService;
        private readonly IUnitOfWork _unitOfWork;

        public UsersService(IMapper mapper, SignInManager<User> signInManager, JwtHandler jwtHandler, IImagesService imagesService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _jwtHandler = jwtHandler;
            _imagesService = imagesService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdWithDetailsAsync(id);

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

            var result = await _unitOfWork.UsersRepository.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join("\n", result
                    .Errors.Select(c => c.Description));

                throw new GameStoreException(errors);
            }

            await _signInManager.SignInAsync(user, false);
            var claims = new List<Claim>
            {
                new Claim("user-name", model.UserName)
            };

            var token = _jwtHandler.GetJwtToken(claims);

            return new UserDto {UserId = user.Id, Token = token};
        }

        public async Task<UserDto> LoginAsync(LoginModel model)
        {
            var user = await _unitOfWork.UsersRepository.GetByNameAsync(model.UserName);

            if (user == null)
            {
                throw new GameStoreException($"User with username {model.UserName} doesn't exist.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsPersistent, false);

            if (!result.Succeeded)
            {
                throw new GameStoreException("Incorrect user's credentials.");
            }

            var claims = new List<Claim>
            {
                new Claim("user-name", model.UserName)
            };

            var token = _jwtHandler.GetJwtToken(claims);

            return new UserDto { UserId = user.Id, Token = token };
        }

        public async Task AddAvatarAsync(User user, IFormFile avatar, HttpRequest request)
        {
            user.AvatarImagePath = await _imagesService.SaveImageAsync(avatar, MediaPathHelper.PathToUsersAvatars, request);

            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
