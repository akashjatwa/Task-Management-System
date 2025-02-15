using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.API.Data;
using TaskManagement.API.Data.Entities;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Models;

namespace TaskManagement.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskDbContext _context;

        public TaskService(TaskDbContext context)
        {
            _context = context;
        }

        // Return all tasks
        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _context.Tasks
                .Select(t => new TaskItem
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    TaskStatus = t.TaskStatus,
                    TaskCode = t.TaskCode,
                    CreatedAt = t.CreatedAt,
                })
                .ToListAsync();
        }


        // Return single task
        public async Task<TaskItem?> GetTaskItemByTaskCodeAsync(string taskCode)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskCode == taskCode);

            if (task == null) return null;

            return new TaskItem
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                TaskStatus = task.TaskStatus,
                TaskCode = task.TaskCode,
                CreatedAt = task.CreatedAt
            };
        }

        //Create a new Task
        public async Task<TaskItem> CreateTaskAsync(TaskItem taskItem)
        {
            var taskEntity = new TaskEntity()
            {
                Title = taskItem.Title,
                Description = taskItem.Description,
                TaskStatus = taskItem.TaskStatus,
                CreatedAt = DateTime.Now,
                TaskCode = $"TASK-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}"
            };

            _context.Tasks.Add(taskEntity);
            await _context.SaveChangesAsync();

            return new TaskItem
            {
                Id = taskItem.Id,
                TaskCode = taskEntity.TaskCode,
                Title = taskItem.Title,
                Description = taskItem.Description,
                TaskStatus = taskItem.TaskStatus
            };
        }

        public async Task<TaskItem?> UpdateTaskAsync(string taskCode, TaskItem updatetaskItem)
        {
            var existingtask = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskCode == taskCode);
            if (existingtask == null) return null;

            if (!string.IsNullOrEmpty(updatetaskItem.Title))
                existingtask.Title = updatetaskItem.Title;

            if (!string.IsNullOrEmpty(updatetaskItem.Description))
                existingtask.Description = updatetaskItem.Description;

            existingtask.TaskStatus = updatetaskItem.TaskStatus;
            existingtask.CreatedAt = DateTime.Now;

            _context.Tasks.Update(existingtask);
            await _context.SaveChangesAsync();

            return new TaskItem
            {
                Id = existingtask.Id,
                TaskCode = existingtask.TaskCode,
                Title = existingtask.Title,
                Description = existingtask.Description,
                CreatedAt = existingtask.CreatedAt,
                TaskStatus = existingtask.TaskStatus
            };
        }

        //Delete a task by its ID
        public async Task<bool> DeleteTaskAsync(string taskCode)
        {
            var task = await _context.Tasks.FindAsync(taskCode);

            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
