using System;

namespace GameStore.BLL.Infrastructure
{
    public class GameStoreException : Exception
    {
        public GameStoreException()
        {
            
        }

        public GameStoreException(string message) : base(message)
        {
        }

        public GameStoreException(string message, Exception inner)
            : base(message, inner) {}
    }
}
