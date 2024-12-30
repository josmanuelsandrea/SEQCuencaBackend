using System.Net;

namespace ScaneqCuencaBackend.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ApiResponse(T data, string message, HttpStatusCode statusCode)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
