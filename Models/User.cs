using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowerCommerceAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)] // Add max length constraint for the username
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)] // Add max length for role to limit possible values
        public string Role { get; set; } = "User";

        public bool IsEmailVerified { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? ProfilePicture { get; set; }

        public string? Address { get; set; }

        //[Phone] // Validates phone number format
        public string? PhoneNumber { get; set; }

        [JsonIgnore]
        public ICollection<WishlistItem> Wishlist { get; set; } = new List<WishlistItem>();

        public User()
        {
            // Additional initialization logic can go here, if necessary
        }
    }
}
