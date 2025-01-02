using FlowerCommerceAPI.Models;
public class ProductTranslation
{
    public int Id { get; set; }
    public int ProductId { get; set; } // Reference to the Product
    public string Language { get; set; } // e.g., "en-US", "fa-IR"
    public string Name { get; set; } = string.Empty; // Localized name
    public string? Description { get; set; } // Localized description
    public string? Type { get; set; } // Localized type
    public string? Category { get; set; } // Localized category
    public string? WaterRequirementRecommendation { get; set; }
    public string? LightRequirementRecommendation { get; set; }
    public string? TemperatureRequirementRecommendation { get; set; }
    public string? SoilRequirementRecommendation { get; set; }
    public Product Product { get; set; }
}
