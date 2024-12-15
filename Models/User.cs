// 1. User Model with Wishlist Support
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
    public string Username { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public bool IsEmailVerified { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? ProfilePicture { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }

    // Navigation property for Wishlist
    [JsonIgnore]
   public ICollection<WishlistItem> Wishlist { get; set; } = new List<WishlistItem>();

}
}