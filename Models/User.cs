using System;
using System.ComponentModel.DataAnnotations;

namespace FlowerCommerceAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User"; // Default role is "User"

        public bool IsEmailVerified { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? ProfilePicture { get; set; } // URL to profile picture (optional)

        public string? Address { get; set; } // Optional address

        public string? PhoneNumber { get; set; } // Optional phone number
    }
}
