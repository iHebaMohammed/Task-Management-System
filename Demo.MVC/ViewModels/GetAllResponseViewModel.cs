using Demo.APIs.DTOs;
using System.Net;

namespace Demo.MVC.ViewModels
{
    public class GetAllResponseViewModel
    {

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public GetAllResult? Result { get; set; } = new GetAllResult();
    }
}
