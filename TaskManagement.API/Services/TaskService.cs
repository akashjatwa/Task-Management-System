using System.Threading.Tasks;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Models;

namespace TaskManagement.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly List<TaskItem> _tasks = new()
        {
        new TaskItem { Id = 1, Title = "Learn C#", Description = "Complete C# basics", Status = "In Progress", CreatedAt = DateTime.UtcNow.AddDays(-2) },
        new TaskItem { Id = 2, Title = "Build Task API", Description = "Create a simple task management API", Status = "To Do", CreatedAt = DateTime.UtcNow.AddDays(-1) },
        new TaskItem { Id = 3, Title = "Write Unit Tests", Description = "Add unit tests for services", Status = "Completed", CreatedAt = DateTime.UtcNow }
        };

        public IEnumerable<TaskItem> GetTasks()
        {
            return _tasks;
        }

        public TaskItem AddTask(TaskItem task)
        {
            task.Id = _tasks.Count + 1;
            task.CreatedAt = DateTime.UtcNow;
            _tasks.Add(task);
            return task;
        }

        public TaskItem GetTaskById(int id)
        {
            var item = _tasks.FirstOrDefault(t => t.Id == id);

            return item;

        }

        public TaskItem? UpdateTask(int id, TaskItem task)
        {
            var item = _tasks.FirstOrDefault(t => t.Id == id);
            if (item == null) return null;

            item.Title = task.Title;
            item.Description = task.Description;
            item.Status = task.Status;

            return item;
        }

        public bool DeleteTask(int id)
        {
            var item = _tasks.FirstOrDefault(t => t.Id == id);
            if (item == null) return false;

            _tasks.Remove(item);

            return true;
        }
    }
}
