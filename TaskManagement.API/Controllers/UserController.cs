using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Models;
using TaskManagement.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var users = await _userService.GetUserByIdAsync(id);
            if(users == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(users);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser(Users user)
        {
            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId}, createdUser);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> UpdateUser(int id, Users users)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, users);
            if(updatedUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(updatedUser);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _userService.DeleteUserAsync(id);
            if(!isDeleted)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
