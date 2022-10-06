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
        Task SaveChangesAsync();
    }
}
