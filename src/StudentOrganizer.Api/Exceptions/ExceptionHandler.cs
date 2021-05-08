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
			else
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var errorResponse = new ErrorResponse
			{
				Message = exception.Message,
				StatusCode = errorCode
			}.ToString();

			return context.Response.WriteAsync(
				$"\n___ PRODUCTION ERROR RESPONSE ___\n" +
				$"{errorResponse}\n" +
				$"___ DEVELOPMENT ERROR RESPONSE ___\n" +
				$"{exception.Message}\n" +
				$"{exception.InnerException?.Message ?? ""}\n" +
				$"{exception.StackTrace} \n" +
				$"{exception.InnerException?.StackTrace ?? ""}"
				 );
		}
	}
}