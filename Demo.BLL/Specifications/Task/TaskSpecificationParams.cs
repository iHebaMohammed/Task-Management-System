using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Specifications.Task
{
    public class TaskSpecificationParams
    {
        public TaskStatus? Status { get; set; }
        public string? Search { get; set; }
        public string? Sort { get; set; }
        public int PageIndex { get; set; } = 1;

        private const int _maxPageSize = 100;
        private int _pageSize = 10;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value > _maxPageSize ? _maxPageSize : value;
            }
        }
    }

}
