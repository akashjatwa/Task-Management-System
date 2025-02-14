using Microsoft.AspNetCore.Mvc;
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

        // GET: api/<TasksController>
        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _taskService.GetTasks();
            return Ok(tasks);
        }

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if(id == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            var taskItem = _taskService.GetTaskById(id);
            return Ok(taskItem);
        }

        // POST api/<TasksController>
        [HttpPost]
        public IActionResult AddTask([FromBody] TaskItem task)
        {
            if(task == null || string.IsNullOrWhiteSpace(task.Title))
            {
                return BadRequest("Task title is required");
            }

            var createdTask = _taskService.AddTask(task);
            return CreatedAtAction(nameof(GetTasks), new {id = createdTask.Id}, createdTask);
        }

        // PUT api/<TasksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TaskItem task)
        {
            if (task == null || string.IsNullOrWhiteSpace(task.Title))
            {
                return BadRequest("Task title is required");
            }

            var item = _taskService.UpdateTask(id, task);
            if(item == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            return Ok(item);
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _taskService.DeleteTask(id);
            if (!isDeleted)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            return NoContent(); // 204 No Content
        }
    }
}
