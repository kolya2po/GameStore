namespace GameStore.DAL.Repositories
{
    public class BaseRepository
    {
        protected readonly GameStoreDbContext DbContext;

        protected BaseRepository(GameStoreDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
