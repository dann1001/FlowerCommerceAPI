using FlowerCommerceAPI.Models; // for the Product and WishlistItem classes
using FlowerCommerceAPI.Data;   // for the AppDbContext class
using Microsoft.EntityFrameworkCore;

namespace FlowerCommerceAPI.Services
{
    /// <summary>
    /// Interface for wishlist-related operations in the service layer.
    /// </summary>
    public interface IWishlistService
    {
        /// <summary>
        /// Adds a product to the user's wishlist.
        /// </summary>
        /// <param name="userId">The ID of the user adding the product.</param>
        /// <param name="productId">The ID of the product to add.</param>
        /// <returns>True if the product was added, false if the product is already in the wishlist or the user is not found.</returns>
        Task<bool> AddToWishlistAsync(int userId, int productId);

        /// <summary>
        /// Removes a product from the user's wishlist.
        /// </summary>
        /// <param name="userId">The ID of the user removing the product.</param>
        /// <param name="productId">The ID of the product to remove.</param>
        /// <returns>True if the product was removed, false if the product is not in the wishlist or the user is not found.</returns>
        Task<bool> RemoveFromWishlistAsync(int userId, int productId);
    }

    /// <summary>
    /// Service for handling wishlist-related operations.
    /// Implements <see cref="IWishlistService"/> to interact with the database.
    /// </summary>
    public class WishlistService : IWishlistService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="WishlistService"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing user and wishlist data.</param>
        public WishlistService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds a product to the user's wishlist asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user adding the product.</param>
        /// <param name="productId">The ID of the product to add.</param>
        /// <returns>True if the product was added successfully, false if it already exists in the wishlist or the user is not found.</returns>
        public async Task<bool> AddToWishlistAsync(int userId, int productId)
        {
            var user = await _context.Users
                .Include(u => u.Wishlist)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false; // User not found
            }

            user.Wishlist ??= new List<WishlistItem>();

            if (user.Wishlist.Any(w => w.ProductId == productId))
            {
                return false; // Product already in wishlist
            }

            var wishlistItem = new WishlistItem
            {
                UserId = userId,
                ProductId = productId
            };

            user.Wishlist.Add(wishlistItem);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Removes a product from the user's wishlist asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user removing the product.</param>
        /// <param name="productId">The ID of the product to remove.</param>
        /// <returns>True if the product was removed successfully, false if it was not in the wishlist or the user is not found.</returns>
        public async Task<bool> RemoveFromWishlistAsync(int userId, int productId)
        {
            var user = await _context.Users
                .Include(u => u.Wishlist)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false; // User not found
            }

            var wishlistItem = user.Wishlist?.FirstOrDefault(w => w.ProductId == productId);

            if (wishlistItem == null)
            {
                return false; // Product not found in wishlist
            }

            user.Wishlist.Remove(wishlistItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
