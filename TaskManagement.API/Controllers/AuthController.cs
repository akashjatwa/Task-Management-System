using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Services;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Hardcoded user validation for now (replace with database check)
            if (request.Email == "admin@example.com" && request.Password == "password")
            {
                var token = _authService.GenerateToken(request.Email);
                return Ok(new { Token = token });
            }

            return Unauthorized(new { Message = "Invalid credentials" });
        }

    }
}
