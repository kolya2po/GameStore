using System;
using System.Net;
using System.Threading.Tasks;
using GameStore.BLL.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace GameStore.WebApi.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ErrorHandlingMiddleware(RequestDelegate requestDelegate, IWebHostEnvironment hostingEnvironment)
        {
            _requestDelegate = requestDelegate;
            _hostingEnvironment = hostingEnvironment;
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
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                if (_hostingEnvironment.IsDevelopment())
                {
                    await context.Response.WriteAsync(ex.Message);
                }

                await context.Response.WriteAsync("Internal server error occurred.");
            }
        }
    }
}
