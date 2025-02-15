namespace TaskManagement.API.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Status TaskStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string TaskCode { get;  set; }
    }

    public enum Status
    {
        ToDo,
        InProgress,
        Completed
    }
}
