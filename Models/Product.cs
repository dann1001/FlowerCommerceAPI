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
        /// Gets or sets the Persian name of the product.
        /// </summary>
        public string? NamePe { get; set; }

        /// <summary>
        /// Gets or sets the Persian price of the product.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal? PricePe { get; set; }

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
        /// Gets or sets the alternate picture URL for the product.
        /// </summary>
        public string? Picture { get; set; }

        /// <summary>
        /// Gets or sets the Persian type of the product.
        /// </summary>
        public string? TypePe { get; set; }

        /// <summary>
        /// Gets or sets the Persian category name for the product.
        /// </summary>
        public string? CategoryPe { get; set; }

        /// <summary>
        /// Gets or sets the favorite status of the product.
        /// </summary>
        public bool Favorite { get; set; }

        /// <summary>
        /// Gets or sets the English name of the product.
        /// </summary>
        public string? NameEn { get; set; }

        /// <summary>
        /// Gets or sets the English type of the product.
        /// </summary>
        public string? TypeEn { get; set; }

        /// <summary>
        /// Gets or sets the English category name for the product.
        /// </summary>
        public string? CategoryEn { get; set; }

        /// <summary>
        /// Gets or sets the English price of the product.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal? PriceEn { get; set; }

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
