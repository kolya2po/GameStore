using System;

namespace GameStore.BLL.Infrastructure
{
    public class GameStoreException : Exception
    {
        public GameStoreException(string message) : base(message)
        {
        }
    }
}
