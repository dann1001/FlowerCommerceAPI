using Microsoft.AspNetCore.Mvc;
using FlowerCommerceAPI.Services;
using FlowerCommerceAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace FlowerCommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly PasswordService _passwordService;
        private static List<User> Users = new List<User>();

        public AuthController(JwtService jwtService, PasswordService passwordService)
        {
            _jwtService = jwtService;
            _passwordService = passwordService;
        }

        // 1. User Registration
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (Users.Exists(u => u.Email == user.Email))
            {
                return Conflict("Email is already in use.");
            }

            // Hash password and store in the PasswordHash field
            user.PasswordHash = _passwordService.HashPassword(user, user.PasswordHash);
            user.Role = "User"; // Default role for new users
            Users.Add(user);

            return Ok("Registration successful");
        }

        // 2. User Login
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.PasswordHash))
            {
                return BadRequest("Username and Password are required.");
            }

            // Find user by username
            var existingUser = Users.Find(u => u.Username == user.Username);

            // Verify password using PasswordHash
            if (existingUser == null || !_passwordService.VerifyPassword(existingUser, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = _jwtService.GenerateToken(existingUser.Username, existingUser.Role);
            return Ok(new { Token = token });
        }

        // 3. Password Reset Flow
        [AllowAnonymous]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword(string email, string newPassword)
        {
            var user = Users.Find(u => u.Email == email);
            if (user == null) return NotFound("User not found");

            // Reset password and hash the new password
            user.PasswordHash = _passwordService.HashPassword(user, newPassword);
            return Ok("Password reset successfully");
        }
    }
}
