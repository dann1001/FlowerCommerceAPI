// DTO for Product creation
public class CreateProductDto
{
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public List<ProductTranslationDto> Translations { get; set; } = new();
}
