using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected readonly IMapper Mapper;

        public BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
