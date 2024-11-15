using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string Title { get; set; }
        public string ? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Pending;
    }
}
