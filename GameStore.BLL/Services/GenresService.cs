using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class GenresService : BaseService, IGenresService
    {
        public GenresService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<GenreModel>> GetAllAsync()
        {
            var genres = await UnitOfWork.GenresRepository.GetAllAsync();

            return Mapper.Map<IEnumerable<GenreModel>>(genres);
        }

        public async Task<GenreModel> GetByIdAsync(int id)
        {
            var genre = await UnitOfWork.GenresRepository.GetByIdAsync(id);

            return Mapper.Map<GenreModel>(genre);
        }

        public async Task<GenreModel> CreateAsync(GenreModel model)
        {
            await UnitOfWork.GenresRepository.CreateAsync(Mapper.Map<Genre>(model));

            await UnitOfWork.SaveChangesAsync();

            return model;
        }

        public async Task UpdateAsync(GenreModel model)
        {
            UnitOfWork.GenresRepository.Update(Mapper.Map<Genre>(model));

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await UnitOfWork.GenresRepository.DeleteByIdAsync(modelId);

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task AddGenreToGameAsync(GameModel gameModel, GenreModel genreModel)
        {
            if (genreModel.ParentGenreId == null && genreModel.SubGenres.Any())
            {
                throw new GameStoreException("You cannot assign a parent genre to the game.");
            }

            await ValidateIfGameAlreadyHasGenre(gameModel.Id, genreModel.Id);
           
            var gameGenre = new GameGenre
            {
                GameId = gameModel.Id,
                GenreId = genreModel.Id
            };

            await UnitOfWork.GameGenresRepository.CreateAsync(gameGenre);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task RemoveGenreFromGameAsync(GameModel gameModel, GenreModel genreModel)
        {
            await ValidateIfGameDoesNotHaveGenre(gameModel.Id, genreModel.Id);

            var gameGenre = await UnitOfWork.GameGenresRepository.GetByIdAsync(gameModel.Id, genreModel.Id);

            UnitOfWork.GameGenresRepository.Delete(gameGenre);
            await UnitOfWork.SaveChangesAsync();
        }

        private async Task ValidateIfGameAlreadyHasGenre(int gameId, int genreId)
        {
            var gameGenre = await UnitOfWork.GameGenresRepository.GetByIdAsync(gameId, genreId);

            if (gameGenre != null)
            {
                throw new GameStoreException("Game already has this genre.");
            }
        }

        private async Task ValidateIfGameDoesNotHaveGenre(int gameId, int genreId)
        {
            var gameGenre = await UnitOfWork.GameGenresRepository.GetByIdAsync(gameId, genreId);

            if (gameGenre == null)
            {
                throw new GameStoreException("Game doesn't have this genre.");
            }
        }
    }
}