using System.Threading.Tasks;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;

namespace GameStore.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDbContext _dbContext;
        private GamesRepository _gamesRepository;
        private GenresRepository _genresRepository;
        private GameGenresRepository _gameGenresRepository;

        public UnitOfWork(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGamesRepository GamesRepository =>
            _gamesRepository ??= new GamesRepository(_dbContext);

        public IGenresRepository GenresRepository => _genresRepository ??= new GenresRepository(_dbContext);
        public IGameGenresRepository GameGenresRepository => _gameGenresRepository ??= new GameGenresRepository(_dbContext);

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
