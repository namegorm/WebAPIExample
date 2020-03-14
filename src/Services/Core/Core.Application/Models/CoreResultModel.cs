using System.Net;

namespace Core.Application.Models
{
    public class CoreResultModel
    {
        public HttpStatusCode HttpStatusCode { get; }
        public string Message { get; }
        public object Data { get; }

        public CoreResultModel(HttpStatusCode httpStatusCode = default, string message = default, object data = default)
        {
            HttpStatusCode = HttpStatusCode;
            Message = message;
            Data = data;
        }
    }
}
