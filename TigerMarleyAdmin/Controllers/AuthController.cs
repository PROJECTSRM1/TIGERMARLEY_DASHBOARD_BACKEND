using Microsoft.AspNetCore.Mvc;
using TigerMarleyAdmin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TigerMarleyAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // In-memory user store
        private static readonly List<LoginRequest> Users = new()
        {
            new LoginRequest { Username = "admin", Email = "admin@gmail.com", Password = "Admin@123" }
        };

        // ---------------- LOGIN ----------------
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "Username and password are required." });

            var user = Users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            if (user != null)
            {
                return Ok(new
                {
                    success = true,
                    message = "Login successful!",
                    user = new { username = user.Username, role = "User" }
                });
            }

            return Unauthorized(new { message = "Invalid username or password." });
        }

        // ---------------- SIGNUP ----------------
        [HttpPost("signup")]
        public IActionResult Signup([FromBody] SignupRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new { message = "Username, email, and password are required." });
            }

            // Gmail validation
            if (!Regex.IsMatch(request.Email, @"^[^\s@]+@gmail\.com$", RegexOptions.IgnoreCase))
            {
                return BadRequest(new { message = "Email must be a valid Gmail address ending with @gmail.com." });
            }

            // Password validation: starts with capital, contains '@', contains at least one number
            if (!Regex.IsMatch(request.Password, @"^[A-Z](?=.*\d)(?=.*@).+$"))
            {
                return BadRequest(new
                {
                    message = "Password must start with a capital letter, include '@', and contain at least one number."
                });
            }

            // Check for duplicate username or email
            if (Users.Any(u => u.Username.ToLower() == request.Username.ToLower()))
                return Conflict(new { message = "Username already exists." });

            if (Users.Any(u => u.Email.ToLower() == request.Email.ToLower()))
                return Conflict(new { message = "Email already registered." });

            Users.Add(new LoginRequest
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            });

            return Ok(new { success = true, message = "Signup successful!" });
        }

        // ---------------- FORGOT PASSWORD ----------------
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            // Only email is required for sending reset link
            if (string.IsNullOrWhiteSpace(request.Email))
                return BadRequest(new { message = "Email is required." });

            // Gmail validation
            if (!Regex.IsMatch(request.Email, @"^[^\s@]+@gmail\.com$", RegexOptions.IgnoreCase))
            {
                return BadRequest(new { message = "Email must be a valid Gmail address ending with @gmail.com." });
            }

            var user = Users.FirstOrDefault(u => u.Email.ToLower() == request.Email.ToLower());
            if (user == null)
                return NotFound(new { message = "User with this email not found." });

            // Simulate sending reset link (in real app, send email)
            return Ok(new { success = true, message = $"Reset link sent to {request.Email}" });
        }

        // ---------------- RESET PASSWORD (Optional Separate Endpoint) ----------------
        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.NewPassword))
                return BadRequest(new { message = "Email and new password are required." });

            var user = Users.FirstOrDefault(u => u.Email.ToLower() == request.Email.ToLower());
            if (user == null)
                return NotFound(new { message = "User with this email not found." });

            // Enforce password rules
            if (!Regex.IsMatch(request.NewPassword, @"^[A-Z](?=.*\d)(?=.*@).+$"))
            {
                return BadRequest(new
                {
                    message = "New password must start with a capital letter, include '@', and contain at least one number."
                });
            }

            user.Password = request.NewPassword;
            return Ok(new { success = true, message = "Password updated successfully!" });
        }
    }
}
