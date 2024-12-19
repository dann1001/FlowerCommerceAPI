using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowerCommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)] // Ensure a max length for name
        public string Name { get; set; } = string.Empty;

        public string? NamePe { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal? PricePe { get; set; }

        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative value.")]
        public int Stock { get; set; }

        public string? ImageUrl { get; set; }

        public string? Picture { get; set; }

        public string? TypePe { get; set; }

        public string? CategoryPe { get; set; }

        public bool Favorite { get; set; }

        public string? NameEn { get; set; }

        public string? TypeEn { get; set; }

        public string? CategoryEn { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal? PriceEn { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public ICollection<WishlistItem> WishlistedBy { get; set; } = new List<WishlistItem>();

        // Maintenance Fields in Persian
        public decimal? WaterRequirementPe { get; set; } // How much water is needed (in Persian)
        public decimal? LightRequirementPe { get; set; } // How much light is needed (in Persian)
        public decimal? TemperatureRequirementPe { get; set; } // Temperature range for care (in Persian)
        public decimal? SoilRequirementPe { get; set; } // Proper soil type or properties (in Persian)

        public decimal? WaterRequirementDetailsPe { get; set; } // Detailed water requirement (in Persian)
        public decimal? LightRequirementDetailsPe { get; set; } // Detailed light requirement (in Persian)
        public decimal? TemperatureRequirementDetailsPe { get; set; } // Detailed temperature requirement (in Persian)
        public decimal? SoilRequirementDetailsPe { get; set; } // Detailed soil requirement (in Persian)

        public string? WaterRequirementRecommendationPe { get; set; } // Recommendation for water requirement (in English)
        public string? LightRequirementRecommendationPe { get; set; } // Recommendation for light requirement (in English)
        public string? TemperatureRequirementRecommendationPe { get; set; } // Recommendation for temperature requirement (in English)
        public string? SoilRequirementRecommendationPe { get; set; } // Recommendation for soil requirement (in English)




        // Maintenance Fields in English
        public decimal? WaterRequirementEn { get; set; } // How much water is needed (in English)
        public decimal? LightRequirementEn { get; set; } // How much light is needed (in English)
        public decimal? TemperatureRequirementEn { get; set; } // Temperature range for care (in English)
        public decimal? SoilRequirementEn { get; set; } // Proper soil type or properties (in English)


        public decimal? WaterRequirementDetailsEn { get; set; } // Detailed water requirement (in English)
        public decimal? LightRequirementDetailsEn { get; set; } // Detailed light requirement (in English)
        public decimal? TemperatureRequirementDetailsEn { get; set; } // Detailed temperature requirement (in English)
        public decimal? SoilRequirementDetailsEn { get; set; } // Detailed soil requirement (in English)


        public string? WaterRequirementRecommendationEn { get; set; } // Recommendation for water requirement (in English)
        public string? LightRequirementRecommendationEn { get; set; } // Recommendation for light requirement (in English)
        public string? TemperatureRequirementRecommendationEn { get; set; } // Recommendation for temperature requirement (in English)
        public string? SoilRequirementRecommendationEn { get; set; } // Recommendation for soil requirement (in English)


        public Product()
        {
            // Any other initialization logic, if necessary
        }
    }
}
