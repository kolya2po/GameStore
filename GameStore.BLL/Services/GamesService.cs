using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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

            if (game == null)
            {
                return null;
            }

            var gameModel = Mapper.Map<GameModel>(game);

            return gameModel;
        }

        public async Task<GameModel> CreateAsync(GameModel model)
        {
            var game = Mapper.Map<Game>(model);

            await UnitOfWork.GamesRepository.CreateAsync(game);
            await UnitOfWork.SaveChangesAsync();


            model.Id = game.Id;
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

        public async Task AddImageAsync(Game game, IFormFile image, HttpRequest request)
        {
            const string pathToFolder = @"D:\Items";

            game.ImagePath = await _imagesService.SaveImageAsync(image, pathToFolder, request);

            await UnitOfWork.SaveChangesAsync();
        }
    }
}
