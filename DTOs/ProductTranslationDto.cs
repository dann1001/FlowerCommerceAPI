public class ProductTranslationDto
{
    public string Language { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? Category { get; set; }
    public string? WaterRequirementRecommendation { get; set; }
    public string? LightRequirementRecommendation { get; set; }
    public string? TemperatureRequirementRecommendation { get; set; }
    public string? SoilRequirementRecommendation { get; set; }
    public List<CreateProductDto> CreateProductDto { get; set; } = new();

}