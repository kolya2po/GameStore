using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGamesRepository GamesRepository { get; }
        IGenresRepository GenresRepository { get; }
        IGameGenresRepository GameGenresRepository { get; }
        ICommentsRepository CommentsRepository { get; }
        ICartsRepository CartsRepository { get; }
        ICartItemsRepository CartItemsRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        IOrderItemsRepository OrderItemsRepository { get; }
        IUsersRepository UsersRepository { get; }
        Task SaveChangesAsync();
    }
}
