using System.Net;
using System.Threading.Tasks;
using GameStore.BLL.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GameStore.WebApi.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ErrorHandlingMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (GameStoreException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
