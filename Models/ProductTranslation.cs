public class ProductTranslation
{
    public int Id { get; set; }
    public int ProductId { get; set; }  // Reference to the Product
    public string Language { get; set; } // e.g., "en-US", "fa-IR"
    public string Name { get; set; }
    public string Description { get; set; }
}
