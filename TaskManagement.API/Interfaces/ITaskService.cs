using TaskManagement.API.Models;

namespace TaskManagement.API.Interfaces
{
    public interface ITaskService
    {
        IEnumerable<TaskItem> GetTasks();

        TaskItem AddTask(TaskItem task);

        TaskItem GetTaskById(int id);

        TaskItem? UpdateTask(int id, TaskItem task);
        
        bool DeleteTask(int id);
    }
}
