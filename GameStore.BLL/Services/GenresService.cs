using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Infrastructure.Exceptions;
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

        public async Task AddGenreToGameAsync(int gameId, int genreId)
        {
            await ValidateIfGameExists(gameId);
            await ValidateIfGenreExistsAndIfItsNotParent(genreId);
            await ValidateIfGameAlreadyHasGenre(gameId, genreId);
           
            var gameGenre = new GameGenre
            {
                GameId = gameId,
                GenreId = genreId
            };

            await UnitOfWork.GameGenresRepository.CreateAsync(gameGenre);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task RemoveGenreFromGameAsync(int gameId, int genreId)
        {
            await ValidateIfGameExists(gameId);
            await ValidateIfGenreExistsAndIfItsNotParent(genreId);
            await ValidateIfGameDoesNotHaveGenre(gameId, genreId);

            var gameGenre = await UnitOfWork.GameGenresRepository.GetByIdAsync(gameId, genreId);

            UnitOfWork.GameGenresRepository.Delete(gameGenre);
            await UnitOfWork.SaveChangesAsync();
        }

        private async Task ValidateIfGameExists(int gameId)
        {
            var game = await UnitOfWork.GamesRepository.GetByIdAsync(gameId);

            if (game == null)
            {
                throw new GameNotFoundException(gameId);
            }
        }

        private async Task ValidateIfGenreExistsAndIfItsNotParent(int genreId)
        {
            var genre = await UnitOfWork.GenresRepository.GetByIdWithDetailsAsync(genreId);

            if (genre == null)
            {
                throw new GenreNotFoundException(genreId);
            }

            if (genre.ParentGenreId == null && genre.SubGenres.Any())
            {
                throw new GameStoreException("You cannot assign a parent genre to the game.");
            }
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