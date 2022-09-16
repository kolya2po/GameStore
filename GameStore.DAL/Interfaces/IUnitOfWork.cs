using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IGamesRepository GamesRepository { get; }
        public IGenresRepository GenresRepository { get; }
        public IGameGenresRepository GameGenresRepository { get; }

        Task SaveChangesAsync();
    }
}
