namespace GameStore.BLL.Infrastructure.Exceptions
{
    public class GenreNotFoundException : NotFoundException
    {
        public GenreNotFoundException(int genreId)
            : base($"Genre with id {genreId} doesn't exist.")
        {
        }
    }
}
