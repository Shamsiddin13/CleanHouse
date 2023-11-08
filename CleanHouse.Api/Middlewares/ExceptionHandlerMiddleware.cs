using CleanHouse.Api.Helpers;
using CleanHouse.Service.Exceptions;

namespace CleanHouse.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CleanHouseException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    Code = ex.StatusCode,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    Code = 500,
                    Message = ex.Message
                });
            }
        }
    }
}
