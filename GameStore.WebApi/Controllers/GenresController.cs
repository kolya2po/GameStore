using AutoMapper;
using GameStore.BLL.Models;
using GameStore.WebApi.Models.Games;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;

namespace GameStore.WebApi.Controllers
{
    public class GenresController : BaseController
    {
        private readonly IGenresService _genresService;
        public GenresController(IMapper mapper, IGenresService genresService) : base(mapper)
        {
            _genresService = genresService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreModel>>> GetAll()
        {
            return Ok(await _genresService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreModel>> GetById(int id)
        {
            return Ok(await _genresService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<GenreModel>> Create(CreateGameDto createGenreDto)
        {
            var genreModel = Mapper.Map<GenreModel>(createGenreDto);

            return Ok(await _genresService.CreateAsync(genreModel));
        }

        [HttpPut]
        public async Task Update(UpdateGameDto updateGenreDto)
        {
            var genreModel = Mapper.Map<GenreModel>(updateGenreDto);
            await _genresService.UpdateAsync(genreModel);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await _genresService.DeleteAsync(id);
        }
    }
}
