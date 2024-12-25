using System.ComponentModel.DataAnnotations;


public class ProductSummaryDTO
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)] // Ensure a max length for name
    public string Name { get; set; } = string.Empty;//
    public string? NamePe { get; set; }//
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public decimal? PricePe { get; set; }//    public decimal Price { get; set; }
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public decimal Price { get; set; }//    public string ImageUrl { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative value.")]
    public int Stock { get; set; }//
    public string? ImageUrl { get; set; } // 
    public string? CategoryPe { get; set; } // 
    public string? CategoryEn { get; set; }//
}
