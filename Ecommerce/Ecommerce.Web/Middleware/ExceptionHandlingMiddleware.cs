using Ecommerce.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Ecommerce.Web.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        /* Công dụng của class này
        Nếu Service: throw new BusinessException(...) 
        --> thì Middleware bắt: catch (BusinessException ex) 
        và trả: 400 Bad Request
        thay vì để ứng dụng biến thành 500.
         */

        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    message = ex.Message
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
        }
    }
}
