using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models.Identity;
using GameStore.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersService _userService;

        public UsersController(IMapper mapper, IUsersService userService) : base(mapper)
        {
            _userService = userService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            return Ok(await _userService.GetByIdAsync(id));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegistrationModel model)
        {
            return Ok(await _userService.RegisterAsync(model));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginModel model)
        {
            return Ok(await _userService.LoginAsync(model));
;       }

        [HttpGet("sign-out")]
        public async Task Logout()
        {
            await _userService.SignOutAsync();
        }

        [HttpPost("{id:int}/avatar")]
        public async Task<ActionResult> AddAvatar(int id, IFormFile avatar)
        {
            var userModel = await _userService.GetByIdAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(new {Path = await _userService.AddAvatarAsync(Mapper.Map<User>(userModel), avatar, Request)});
        }
    }
}
