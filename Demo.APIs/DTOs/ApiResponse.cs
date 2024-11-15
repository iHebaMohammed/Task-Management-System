using System.Net;

namespace Demo.APIs.DTOs
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public object ? Result { get; set; }
    }
}
