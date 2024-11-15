using Demo.DAL.Entities;

namespace Demo.APIs.DTOs
{
    public class TaskDto
    {
        public int ? Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Pending;
    }
}
