using Microsoft.AspNetCore.Mvc;
using FlowerCommerceAPI.Services;
using FlowerCommerceAPI.Models;
using System.Collections.Generic;

namespace FlowerCommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private static List<User> Users = new List<User> // For demo purposes
        {
            new User { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" }
        };

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var existingUser = Users.Find(u => u.Username == user.Username && u.Password == user.Password);

            if (existingUser == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var token = _jwtService.GenerateToken(existingUser.Username, existingUser.Role);
            return Ok(new { Token = token });
        }
    }
}
