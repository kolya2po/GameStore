using System;

namespace GameStore.BLL.Infrastructure
{
    public class NotFoundException : Exception
    {
        public int StatusCode { get; protected set; } = 404;

        protected NotFoundException(string message) : base(message)
        {
            
        }
    }
}
