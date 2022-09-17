using System;

namespace GameStore.BLL.Infrastructure.Exceptions
{
    public class InvalidFileContentTypeException : GameStoreException
    {
        public InvalidFileContentTypeException() :
            base("You should send an image.") { }
    }
}
