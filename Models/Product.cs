using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowerCommerceAPI.Models
{
    /// <summary>
    /// Represents a product in the system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        [Required]
        [MaxLength(100)] // Ensure a max length for name
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity available.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative value.")]
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the URL of the product image.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated category.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Navigation property for the wishlist items that have this product.
        /// </summary>
        [JsonIgnore]
        public ICollection<WishlistItem> WishlistedBy { get; set; } = new List<WishlistItem>();

        // Optional: Constructor for initializing properties
        public Product()
        {
            // Any other initialization logic, if necessary
        }
    }
}
