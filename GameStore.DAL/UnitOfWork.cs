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
        private CommentsRepository _commentsRepository;
        private CartsRepository _cartsRepository;
        private CartItemsRepository _cartItemsRepository;
        private OrdersRepository _ordersRepository;
        private OrderItemsRepository _orderItemsRepository;

        public UnitOfWork(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGamesRepository GamesRepository =>
            _gamesRepository ??= new GamesRepository(_dbContext);

        public IGenresRepository GenresRepository => _genresRepository ??= new GenresRepository(_dbContext);
        public IGameGenresRepository GameGenresRepository => _gameGenresRepository ??= new GameGenresRepository(_dbContext);
        public ICommentsRepository CommentsRepository =>
            _commentsRepository ??= new CommentsRepository(_dbContext);

        public ICartsRepository CartsRepository =>
            _cartsRepository ??= new CartsRepository(_dbContext);

        public ICartItemsRepository CartItemsRepository =>
            _cartItemsRepository ??= new CartItemsRepository(_dbContext);

        public IOrdersRepository OrdersRepository =>
            _ordersRepository ??= new OrdersRepository(_dbContext);

        public IOrderItemsRepository OrderItemsRepository =>
            _orderItemsRepository ??= new OrderItemsRepository(_dbContext);

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
