using Microsoft.AspNetCore.Mvc;
using FlowerCommerceAPI.Services;
using FlowerCommerceAPI.Models;
using FlowerCommerceAPI.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace FlowerCommerceAPI.Controllers
{
    /// <summary>
    /// Handles authentication and user management tasks, including user registration, login, password reset, and admin-only actions.
    /// </summary>
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

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user details for registration.</param>
        /// <returns>A response indicating success or failure of the registration process.</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            // Check if Users is null
            if (_dbContext.Users == null)
            {
                return StatusCode(500, "Database connection issue.");
            }

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

        /// <summary>
        /// Logs in an existing user and generates a JWT token.
        /// </summary>
        /// <param name="user">The login credentials of the user.</param>
        /// <returns>A response containing a JWT token if login is successful, or an error message if not.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.PasswordHash))
            {
                return BadRequest("Username and Password are required.");
            }

            // Check if Users is null
            if (_dbContext.Users == null)
            {
                return StatusCode(500, "Database connection issue.");
            }

            // Find user by username (check for null explicitly)
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Username == user.Username);

            // Verify password
            if (existingUser == null || !_passwordService.VerifyPassword(existingUser, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password");
            }

            // Generate JWT token
            var token = _jwtService.GenerateToken(existingUser.Username, existingUser.Role, existingUser.Id); // Use 'false' for regular sessions
            return Ok(new { Token = token });
        }

        /// <summary>
        /// Resets a user's password (Admin only).
        /// </summary>
        /// <param name="email">The email of the user whose password is to be reset.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        /// <returns>A response indicating the success or failure of the password reset process.</returns>
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword(string email, string newPassword)
        {
            if (_dbContext.Users == null)
            {
                return StatusCode(500, "Database connection issue.");
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return NotFound("User not found");

            // Reset password and hash the new one
            user.PasswordHash = _passwordService.HashPassword(user, newPassword);
            _dbContext.SaveChanges();

            return Ok("Password reset successfully");
        }

        /// <summary>
        /// Gets a list of all users (Admin only).
        /// </summary>
        /// <returns>A list of users with their ID, username, email, and role.</returns>
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            if (_dbContext.Users == null)
            {
                return StatusCode(500, "Database connection issue.");
            }

            var users = _dbContext.Users.Select(u => new { u.Id, u.Username, u.Email, u.Role }).ToList();
            return Ok(users);
        }
    }
}
