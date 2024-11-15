using Demo.APIs.DTOs;
using System.Net;

namespace Demo.MVC.ViewModels
{
    public class ResponseViewModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public TaskDto? Result { get; set; }
    }
}
