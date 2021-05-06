using System;

namespace StudentOrganizer.Core.Common
{
	public class AppException : Exception
	{
		public readonly AppErrorCode ErrorCode;

		public AppException(string message, AppErrorCode errorCode, params object[] args)
			: this(message, errorCode, null, args)
		{
		}

		public AppException(string message, AppErrorCode errorCode, Exception innerException, params object[] args)
			: base(string.Format(message, args), innerException)
		{
			ErrorCode = errorCode;
		}
	}
}