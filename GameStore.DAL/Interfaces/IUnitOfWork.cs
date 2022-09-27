using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IGamesRepository GamesRepository { get; }
        public IGenresRepository GenresRepository { get; }
        public IGameGenresRepository GameGenresRepository { get; }
        public ICommentsRepository CommentsRepository { get; }

        Task SaveChangesAsync();
    }
}
