using FlowerCommerceAPI.Models; // for the Product and WishlistItem classes
using FlowerCommerceAPI.Data;   // for the AppDbContext class
using Microsoft.EntityFrameworkCore;

namespace FlowerCommerceAPI.Services
{
    public interface IWishlistService
    {
        Task<bool> AddToWishlistAsync(int userId, int productId);

        Task<bool> RemoveFromWishlistAsync(int userId, int productId);
    }

    public class WishlistService : IWishlistService
    {
        private readonly AppDbContext _context;

        public WishlistService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

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
