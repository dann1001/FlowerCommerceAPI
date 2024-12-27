using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowerCommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // Localized name

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; } // Localized price

        public string? Description { get; set; } // Localized description

        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative value.")]
        public int Stock { get; set; }

        public string? ImageUrl { get; set; } // Single property for image URL

        public string? Type { get; set; } // Localized type
        public string? Category { get; set; } // Localized category
        public bool Favorite { get; set; }

        public int CategoryId { get; set; } // Foreign key for category

        [JsonIgnore]
        public ICollection<WishlistItem> WishlistedBy { get; set; } = new List<WishlistItem>();

        // Maintenance Fields
        public decimal? WaterRequirement { get; set; }
        public decimal? LightRequirement { get; set; }
        public decimal? TemperatureRequirement { get; set; }
        public decimal? SoilRequirement { get; set; }

        public decimal? WaterRequirementDetails { get; set; }
        public decimal? LightRequirementDetails { get; set; }
        public decimal? TemperatureRequirementDetails { get; set; }
        public decimal? SoilRequirementDetails { get; set; }

        public string? WaterRequirementRecommendation { get; set; }
        public string? LightRequirementRecommendation { get; set; }
        public string? TemperatureRequirementRecommendation { get; set; }
        public string? SoilRequirementRecommendation { get; set; }
    }
}
