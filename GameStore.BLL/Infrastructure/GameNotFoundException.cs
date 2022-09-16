namespace GameStore.BLL.Infrastructure
{
    public class GameNotFoundException : NotFoundException
    {
        public GameNotFoundException(int gameId) 
            : base($"Game with id {gameId} doesn't exist.")
        {
        }
    }
}
