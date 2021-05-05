using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StudentOrganizer.Api.Exceptions
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate next;

        public ExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result = new ErrorResponse()
            {
                Message = exception.Message,
                StatusCode = (int) HttpStatusCode.BadRequest
            }.ToString();
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }
    }
}
