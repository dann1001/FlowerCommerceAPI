using Microsoft.AspNetCore.Mvc;
using FlowerCommerceAPI.Services;
using FlowerCommerceAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cryptography.KeyDerivation; // For password hashing

namespace FlowerCommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private static List<User> Users = new List<User> // For demo purposes, replace with database in production
        {
            new User { Id = 1, Username = "admin", Password = HashPassword("admin123"), Role = "Admin" }
        };

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Username and Password are required.");
            }

            var existingUser = Users.Find(u => u.Username == user.Username);
            
            if (existingUser == null || !VerifyPassword(user.Password, existingUser.Password))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = _jwtService.GenerateToken(existingUser.Username, existingUser.Role);
            return Ok(new { Token = token });
        }

        // Hash password (for storage)
        private static string HashPassword(string password)
        {
            // Salt and hash the password
            byte[] salt = new byte[128 / 8];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        // Verify password (during login)
        private static bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            var parts = storedPassword.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            string enteredHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return storedHash == enteredHash;
        }
    }
}
