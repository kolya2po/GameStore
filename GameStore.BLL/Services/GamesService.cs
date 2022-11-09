using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GameStore.BLL.Services
{
    public class GamesService : BaseService, IGamesService
    {
        private readonly IImagesService _imagesService;
        public GamesService(IUnitOfWork unitOfWork, IMapper mapper, IImagesService imagesService) : base(unitOfWork, mapper)
        {
            _imagesService = imagesService;
        }

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

            return Mapper.Map<GameModel>(game);
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

        public async Task<string> AddImageAsync(GameModel gameModel, IFormFile image, HttpRequest request)
        {
            var imagePath = await _imagesService.SaveImageAsync(image, MediaPathHelper.PathToGamesImages, request);

            gameModel.ImagePath = imagePath;

            await UpdateAsync(gameModel);

            return imagePath;
        }
    }
}
