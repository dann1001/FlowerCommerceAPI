using Microsoft.AspNetCore.Mvc;
using FlowerCommerceAPI.Services;
using FlowerCommerceAPI.Models;
using FlowerCommerceAPI.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace FlowerCommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly JwtService _jwtService;
        private readonly PasswordService _passwordService;

        public AuthController(AppDbContext dbContext, JwtService jwtService, PasswordService passwordService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
            _passwordService = passwordService;
        }

        // 1. User Registration - Public
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (_dbContext.Users.Any(u => u.Email == user.Email))
            {
                return Conflict("Email is already in use.");
            }

            // Hash the password
            user.PasswordHash = _passwordService.HashPassword(user, user.PasswordHash);
             // Role assignment logic
            if (string.IsNullOrEmpty(user.Role) || (user.Role != "Admin" && user.Role != "User"))
            {
                user.Role = "User"; // Default role if not provided or invalid
            }

            // Save to database
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Ok("Registration successful");
        }

        // 2. User Login - Public
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.PasswordHash))
            {
                return BadRequest("Username and Password are required.");
            }

            // Find user by username
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Username == user.Username);

            // Verify password
            if (existingUser == null || !_passwordService.VerifyPassword(existingUser, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password");
            }

            // Generate JWT token
            var token = _jwtService.GenerateToken(existingUser.Username, existingUser.Role);
            return Ok(new { Token = token });
        }

        // 3. Password Reset Flow - Admin Only
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword(string email, string newPassword)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return NotFound("User not found");

            // Reset password and hash the new one
            user.PasswordHash = _passwordService.HashPassword(user, newPassword);
            _dbContext.SaveChanges();

            return Ok("Password reset successfully");
        }

        // 4. Admin-Only User List (Example Endpoint)
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _dbContext.Users.Select(u => new { u.Id, u.Username, u.Email, u.Role }).ToList();
            return Ok(users);
        }
    }
}
