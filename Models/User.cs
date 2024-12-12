// 1. User Model with Wishlist Support
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlowerCommerceAPI.Models
{
    public class User
    {
        public int Id { get; set; } // Unique identifier for the user

        [Required] // Username is a required field
        public string Username { get; set; } = string.Empty;

        [Required] // Email is a required field
        [EmailAddress] // Validates email format
        public string Email { get; set; } = string.Empty;

        [Required] // Password is required
        [DataType(DataType.Password)] // Indicates the field is for passwords
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User"; // Default role is "User"

        public bool IsEmailVerified { get; set; } = false; // Tracks if the user's email is verified

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Tracks when the account was created

        public string? ProfilePicture { get; set; } // URL to profile picture (optional)

        public string? Address { get; set; } // Optional address

        public string? PhoneNumber { get; set; } // Optional phone number

        // Wishlist Feature: Stores a list of product IDs
        public ICollection<int> Wishlist { get; set; } = new List<int>();
    }
}