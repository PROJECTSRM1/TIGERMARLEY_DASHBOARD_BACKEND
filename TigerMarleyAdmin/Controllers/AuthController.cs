using Microsoft.AspNetCore.Mvc;
using TigerMarleyAdmin.Data;
using TigerMarleyAdmin.Models;
using System.Linq;

namespace TigerMarleyAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;

            if (!_context.Users.Any())
            {
                _context.Users.Add(new User
                {
                    Username = "admin",
                    Password = "admin123",
                    Role = "Admin"
                });
                _context.SaveChanges();
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Username == request.Username && u.Password == request.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(new
            {
                message = "Login successful",
                username = user.Username,
                role = user.Role
            });
        }

        [HttpPost("signup")]
        public IActionResult Signup([FromBody] SignupRequest request)
        {
            if (_context.Users.Any(u => u.Username == request.Username))
                return BadRequest(new { message = "Username already exists" });

            var newUser = new User
            {
                Username = request.Username,
                Password = request.Password,
                Role = "User"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(new { message = $"Password reset link sent to {request.Username}'s email (mock)." });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class SignupRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class ForgotPasswordRequest
    {
        public string Username { get; set; } = string.Empty;
    }
}
