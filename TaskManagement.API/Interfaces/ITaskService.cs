using TaskManagement.API.Models;

namespace TaskManagement.API.Interfaces
{
    public interface ITaskService
    {
        /* List<TaskItem> GetTasks();

         void UpdateTask(TaskItem updatedTask);

         void DeleteTask(int id);

         TaskItem GetTaskById(int id);*/

        Task<IEnumerable<TaskItem>> GetAllTasksAsync();

        Task<TaskItem?> GetTaskItemByIdAsync(int id);

        Task<TaskItem> CreateTaskAsync(TaskItem taskItem); //Adding a new task

        Task<TaskItem?> UpdateTaskAsync(int id , TaskItem updatetaskItem);

        Task<bool> DeleteTaskAsync(int id);
    }
}
