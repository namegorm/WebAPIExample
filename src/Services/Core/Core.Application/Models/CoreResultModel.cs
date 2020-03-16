using System.Net;

namespace Core.Application.Models
{
    public class CoreResultModel
    {
        public int HttpStatusCode { get; }
        public string Message { get; }
        public object Data { get; }

        public CoreResultModel(HttpStatusCode httpStatusCode, string message = default, object data = default)
        {
            HttpStatusCode = (int)httpStatusCode;
            Message = message;
            Data = data;
        }
    }
}
