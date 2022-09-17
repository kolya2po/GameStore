namespace GameStore.BLL.Infrastructure.Exceptions
{
    public class GameNotFoundException : NotFoundException
    {
        public GameNotFoundException(int gameId)
            : base($"Game with id {gameId} doesn't exist.")
        {
        }
    }
}
