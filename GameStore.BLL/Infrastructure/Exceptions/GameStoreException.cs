using System;

namespace GameStore.BLL.Infrastructure.Exceptions
{
    public class GameStoreException : Exception
    {
        public GameStoreException(string message) : base(message)
        {
        }
    }
}
