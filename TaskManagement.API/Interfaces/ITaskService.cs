using TaskManagement.API.Models;

namespace TaskManagement.API.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();

        Task<TaskItem?> GetTaskItemByTaskCodeAsync(string taskCode);

        Task<TaskItem> CreateTaskAsync(TaskItem taskItem); //Adding a new task

        Task<TaskItem?> UpdateTaskAsync(string taskCode, TaskItem updatetaskItem);

        Task<bool> DeleteTaskAsync(string taskCode);
    }
}
