namespace FlowerCommerceAPI.Models
{
    /// <summary>
    /// Represents an item in a user's wishlist.
    /// </summary>
    public class WishlistItem
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user who owns this wishlist item.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property for the associated user.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the product added to the wishlist.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Navigation property for the associated product.
        /// </summary>
        public Product? Product { get; set; }
    }
}
