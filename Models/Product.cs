namespace FlowerCommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; } // Unique identifier for the product

        public string? Name { get; set; } // Name of the product
        public string? Description { get; set; } // Detailed description of the product
        public decimal Price { get; set; } // Price of the product
        public int Stock { get; set; } // Stock quantity available

        public string? ImageUrl { get; set; } // URL of the product image
        public int CategoryId { get; set; } // ID of the associated category
       public IEnumerable<WishlistItem> WishlistedBy { get; set; } = new List<WishlistItem>();

     }}