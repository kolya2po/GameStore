namespace GameStore.BLL.Infrastructure
{
    public class InvalidFileContentTypeException : GameStoreException
    {
        public InvalidFileContentTypeException() :
            base("You should send an image.")
        { }
    }
}
