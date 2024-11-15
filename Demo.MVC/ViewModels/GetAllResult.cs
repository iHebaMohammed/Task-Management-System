using Demo.APIs.DTOs;

namespace Demo.MVC.ViewModels
{
    public class GetAllResult
    {
        ///

        //*    "pageIndex": 0,
        //     "pageSize": 0,
        //     "count": 0,
        //     "data": [
        //       {
        //         "title": "string",
        //         "description": "string",
        //         "dueDate": "2024-11-14T21:32:05.896Z",
        //         "status": 0
        //       }
        //     ]

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<TaskDto>? Data { get; set; }
    }
}
