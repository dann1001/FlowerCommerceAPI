using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowerCommerceAPI.Models
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username for the user. Must be unique.
        /// </summary>
        [Required]
        [MaxLength(50)] // Add max length constraint for the username
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password hash for the user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role of the user (e.g., User, Admin).
        /// Default is "User".
        /// </summary>
        [Required]
        [MaxLength(20)] // Add max length for role to limit possible values
        public string Role { get; set; } = "User";

        /// <summary>
        /// Gets or sets whether the user's email is verified.
        /// </summary>
        public bool IsEmailVerified { get; set; } = false;

        /// <summary>
        /// Gets or sets the date and time when the user was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the URL of the user's profile picture.
        /// </summary>
        public string? ProfilePicture { get; set; }

        /// <summary>
        /// Gets or sets the user's address.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        //[Phone] // Validates phone number format
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Navigation property for the user's wishlist.
        /// </summary>
        [JsonIgnore]
        public ICollection<WishlistItem> Wishlist { get; set; } = new List<WishlistItem>();

        // Optional: Constructor for initialization
        public User()
        {
            // Additional initialization logic can go here, if necessary
        }
    }
}
