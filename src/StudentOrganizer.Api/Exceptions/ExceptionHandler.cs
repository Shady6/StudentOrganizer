using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StudentOrganizer.Core.Common;

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
            var errorCode = AppErrorCode.DEFAULT_ERROR;            
			if (exception is AppException e)
			{
                errorCode = e.ErrorCode;
                if (e.ErrorCode == AppErrorCode.CANT_DO_THAT)
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                else
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            var errorResponse = new ErrorResponse
            {
                Message = exception.Message,
                StatusCode = errorCode
            }.ToString();

            throw new Exception($"\n___ PRODUCTION ERROR RESPONSE ___\n{errorResponse}\n___DEVELOPMENT ERROR RESPONSE___\n{exception.Message}", exception);

            // TODO uncomment when development is done
            // return context.Response.WriteAsync(errorResponse);
        }
    }
}
