using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebApi.Models.Games;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Query;

namespace GameStore.WebApi.Controllers
{
    public class GamesController : BaseController
    {
        private readonly IGamesService _gamesService;
        private readonly IGenresService _genresService;

        public GamesController(IMapper mapper, IGamesService gamesService, IGenresService genresService) : base(mapper)
        {
            _gamesService = gamesService;
            _genresService = genresService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<GameModel>>> GetAll()
        {
            return Ok(await _gamesService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GameModel>> GetById(int id)
        {
            return Ok(await _gamesService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<GameModel>> Create(CreateGameDto createGameDto)
        {
            var gameModel = Mapper.Map<GameModel>(createGameDto);

            return Ok(await _gamesService.CreateAsync(gameModel));
        }

        [HttpPut]
        public async Task Update(UpdateGameDto updateGameDto)
        {
            var gameModel = Mapper.Map<GameModel>(updateGameDto);
            await _gamesService.UpdateAsync(gameModel);
        }

        [HttpPost("{gameId:int}/image")]
        public async Task AddImage(int gameId, IFormFile image)
        {
            await _gamesService.AddImageAsync(gameId, image, Request);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await _gamesService.DeleteAsync(id);
        }

        [HttpPost("{gameId:int}/genre/{genreId:int}")]
        public async Task AddGenre(int gameId, int genreId)
        {
            await _genresService.AddGenreToGameAsync(gameId, genreId);
        }

        [HttpDelete("{gameId:int}/genre/{genreId:int}")]
        public async Task RemoveGenre(int gameId, int genreId)
        {
            await _genresService.RemoveGenreFromGameAsync(gameId, genreId);
        }
    }
}
