namespace FlowerCommerceAPI.Models
{
    public class WishlistItem
    {
        public int UserId { get; set; }

        public User? User { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }
    }
}
