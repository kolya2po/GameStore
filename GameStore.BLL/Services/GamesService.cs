using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Infrastructure.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GameStore.BLL.Services
{
    public class GamesService : BaseService, IGamesService
    {
        public GamesService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<GameModel>> GetAllAsync()
        {
            var games = await UnitOfWork.GamesRepository.GetAllWithDetailsAsync();

            return Mapper.Map<IEnumerable<GameModel>>(games);
        }

        public async Task<GameModel> GetByIdAsync(int id)
        {
            var game = await UnitOfWork.GamesRepository.GetByIdWithDetailsAsync(id);

            return Mapper.Map<GameModel>(game);
        }

        public async Task<GameModel> CreateAsync(GameModel model)
        {
            var game = Mapper.Map<Game>(model);

            await UnitOfWork.GamesRepository.CreateAsync(game);
            await UnitOfWork.SaveChangesAsync();

            return model;
        }

        public async Task UpdateAsync(GameModel model)
        {
            UnitOfWork.GamesRepository.Update(Mapper.Map<Game>(model));

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await UnitOfWork.GamesRepository.DeleteByIdAsync(modelId);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task AddImageAsync(int gameId, IFormFile image, HttpRequest request)
        {
            var game = await UnitOfWork.GamesRepository.GetByIdAsync(gameId);

            ValidateParameters(game, gameId, image);

            var imageFormat = image.FileName.Split('.')[^1];
            const string pathToFolder = @"D:\Items";

            var fileName = $"{Guid.NewGuid()}.{imageFormat}";
            var filePath = Path.Combine(pathToFolder, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);

            await image.CopyToAsync(stream);

            var domainName = $"{request.Scheme}://{request.Host.Value}";

            game.ImagePath = $"{domainName}/{fileName}";

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<GameModel>> GetGamesByFilterAsync(FilterSearchModel filterModel)
        {
            if (filterModel == null)
            {
                throw new GameStoreException("Filter model was null.");
            }

            var games = await UnitOfWork.GamesRepository.GetAllWithDetailsAsync();

            var result = Mapper.Map<IEnumerable<GameModel>>(games);

            if (filterModel.Genres != null && filterModel.Genres.Any())
            {
                result = await GetGamesByGenresAsync(filterModel.Genres);
            }

            if (!string.IsNullOrEmpty(filterModel.GameName))
            {
                result = result.Where(c => c.Name.Contains(filterModel.GameName));
            }

            return result;
        }

        private static void ValidateParameters(Game game, int gameId, IFormFile file)
        {
            if (file == null)
            {
                throw new GameStoreException("Image was null.");
            }

            if (!file.ContentType.Contains("image"))
            {
                throw new InvalidFileContentTypeException();
            }

            if (game == null)
            {
                throw new GameNotFoundException(gameId);
            }
        }
        private async Task<IEnumerable<GameModel>> GetGamesByGenresAsync(IEnumerable<string> genresToFind)
        {
            genresToFind = genresToFind.Select(c => c.ToLower());

            var allGenres = await UnitOfWork.GenresRepository.GetAllWithDetailsAsync();

            var genres = allGenres.Where(c => genresToFind.Contains(c.Name.ToLower())).ToArray();

            var gamesFromGenres = genres
                .Where(c => c.Games != null)
                .SelectMany(c => c.Games)
                .Select(c => c.Game);

            var gamesFromSubGenres = genres
                .Where(c => c.Games == null)
                .SelectMany(c => c.SubGenres)
                .SelectMany(c => c.Games)
                .Select(c => c.Game);

            var games = gamesFromGenres.Union(gamesFromSubGenres);

            return Mapper.Map<IEnumerable<GameModel>>(games);
        }
    }
}
