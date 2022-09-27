using System;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebApi.Models.Games;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.DAL.Entities;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<GameModel>>> GetAll()
        {
            return Ok(await _gamesService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GameModel>> GetById(int id)
        {
            var game = await _gamesService.GetByIdAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameModel>> Create(CreateGameDto createGameDto)
        {
            var gameModel = Mapper.Map<GameModel>(createGameDto);

            return Ok(await _gamesService.CreateAsync(gameModel));
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateGameDto updateGameDto)
        {
            var game = await _gamesService.GetByIdAsync(updateGameDto.Id);

            if (game == null)
            {
                return NotFound();
            }

            var gameModel = Mapper.Map<GameModel>(updateGameDto);
            await _gamesService.UpdateAsync(gameModel);

            return Ok();
        }

        [HttpPost("{gameId:int}/image")]
        public async Task<ActionResult> AddImage(int gameId, IFormFile image)
        {
            var gameModel = await _gamesService.GetByIdAsync(gameId);

            if (gameModel == null)
            {
                return NotFound();
            }

            await _gamesService.AddImageAsync(Mapper.Map<Game>(gameModel),
                image, Request);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _gamesService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{gameId:int}/genre/{genreId:int}")]
        public async Task<ActionResult> LinkGenreToGame(int gameId, int genreId)
        {
            var game = await _gamesService.GetByIdAsync(gameId);
            var genre = await _genresService.GetByIdAsync(genreId);

            if (game == null)
            {
                return NotFound();
            }

            if (genre == null)
            {
                return NotFound();
            }

            await _genresService.AddGenreToGameAsync(game, genre);
            return Ok();
        }


        [HttpDelete("{gameId:int}/genre/{genreId:int}")]
        public async Task<ActionResult> UnlinkGenreFromGame(int gameId, int genreId)
        {
            var game = await _gamesService.GetByIdAsync(gameId);

            if (game == null)
            {
                return NotFound($"Game with id {gameId} doesn't exist.");
            }

            var genre = await _genresService.GetByIdAsync(genreId);

            if (genre == null)
            {
                return NotFound($"Genre with id {genreId} doesn't exist.");
            }

            await _genresService.RemoveGenreFromGameAsync(game, genre);
            return NoContent();
        }
    }
}
