using Newtonsoft.Json;
using StudentOrganizer.Core.Common;

namespace StudentOrganizer.Api.Exceptions
{
    public class ErrorResponse
    {
        public AppErrorCode StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(
				this,
				Formatting.Indented,
				new Newtonsoft.Json.Converters.StringEnumConverter());
        }
    }
}
