using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllTask()
        {
            var task = await _taskService.GetAllTasksAsync();
            return Ok(task);
        }

        [HttpGet("{taskCode}")]
        public async Task<ActionResult<TaskItem>> GetTaskByTaskCode(string taskCode)
        {
            var task = await _taskService.GetTaskItemByTaskCodeAsync(taskCode);
            if (task == null)
            {
                return NotFound($"Task with ID {taskCode} not found.");
            }
            return Ok(task);
        }

        [HttpPut("{taskCode}")]
        public async Task<ActionResult<TaskItem>> UpdateTask(string taskCode, [FromBody] TaskItem taskItem)
        {
            var updatedTask = await _taskService.UpdateTaskAsync(taskCode, taskItem);
            if(updatedTask == null)
            {
                return NotFound($"Task with ID {taskCode} not found.");
            }

            return Ok(updatedTask);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskItem taskItem)
        {
            if (taskItem == null)
            {
                return BadRequest("Task data is required.");
            }

            var createdTask = await _taskService.CreateTaskAsync(taskItem);

            return CreatedAtAction(nameof(CreateTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpDelete("{taskCode}")]
        public async Task<IActionResult> DeleteTask(string taskCode)
        {
            var isDeleted = await _taskService.DeleteTaskAsync(taskCode);
            if (!isDeleted)
            {
                return NotFound($"Task with ID {taskCode} not found.");
            }

            return NoContent();
        }



    
    }
}
