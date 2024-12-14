using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlowerCommerceAPI.Data;
using FlowerCommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowerCommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context; // Injects the database context
        }

        // GET: api/Products (Fetch all products - Public Access)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync(); // Retrieves all products from the database
            return Ok(products); // Returns the list of products as an HTTP 200 response
        }

        // GET: api/Products/5 (Fetch a single product by ID - Public Access)
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id); // Finds a product by ID

            if (product == null)
            {
                return NotFound(); // Returns HTTP 404 if the product is not found
            }

            return Ok(product); // Returns the product as an HTTP 200 response
        }

        // POST: api/Products (Create a new product - Admin Only)
        [Authorize(Roles = "Admin")] // Restricts access to Admins only
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null"); // Returns HTTP 400 if no data is provided
            }

            _context.Products.Add(product); // Adds the product to the database
            await _context.SaveChangesAsync(); // Saves changes to the database

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product); // Returns HTTP 201 with the created product
        }

        // PUT: api/Products/5 (Update an existing product - Admin Only)
        [Authorize(Roles = "Admin")] // Restricts access to Admins only
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch"); // Returns HTTP 400 if IDs don't match
            }

            _context.Entry(product).State = EntityState.Modified; // Marks the product as modified

            try
            {
                await _context.SaveChangesAsync(); // Attempts to save changes
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound(); // Returns HTTP 404 if the product no longer exists
                }
                throw; // Re-throws the exception if it's not related to product existence
            }

            return NoContent(); // Returns HTTP 204 if update is successful
        }

        // DELETE: api/Products/5 (Delete a product - Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id); // Finds the product by ID
            if (product == null)
            {
                return NotFound(); // Returns HTTP 404 if the product is not found
            }

            _context.Products.Remove(product); // Removes the product from the database
            await _context.SaveChangesAsync(); // Saves changes to the database

            return NoContent(); // Returns HTTP 204 if deletion is successful
        }

        // POST: api/Products/Wishlist/Add (Add product to user wishlist)
      [Authorize] // Requires authentication
[HttpPost("wishlist/add/{productId}")]
public async Task<IActionResult> AddToWishlist(int productId)
{
    var userId = int.Parse(User.FindFirst("Id")?.Value ?? "0"); // Extracts the user ID from JWT claims
    var user = await _context.Users.Include(u => u.Wishlist).FirstOrDefaultAsync(u => u.Id == userId); // Loads user and wishlist

    if (user == null)
    {
        return NotFound("User not found"); // Returns HTTP 404 if the user is not found
    }

    // Create the wishlist item
    var wishlistItem = new WishlistItem
    {
        UserId = userId,
        ProductId = productId
    };

    // Check if the product is already in the wishlist
    if (user.Wishlist.Any(w => w.ProductId == productId))
    {
        return BadRequest("Product already in wishlist"); // If the product is already in the wishlist
    }

    // Add to wishlist
    user.Wishlist.Add(wishlistItem); // Adds the product to the wishlist
    await _context.SaveChangesAsync(); // Saves changes to the database

    return Ok("Product added to wishlist"); // Returns HTTP 200 on success
}


        // DELETE: api/Products/Wishlist/Remove (Remove product from user wishlist)
   [Authorize] // Requires authentication
[HttpDelete("wishlist/remove/{productId}")]
public async Task<IActionResult> RemoveFromWishlist(int productId)
{
    var userId = int.Parse(User.FindFirst("Id")?.Value ?? "0"); // Extracts the user ID from JWT claims
    var user = await _context.Users.Include(u => u.Wishlist).FirstOrDefaultAsync(u => u.Id == userId); // Loads user and wishlist

    if (user == null)
    {
        return NotFound("User not found"); // Returns HTTP 404 if the user is not found
    }

    // Find the wishlist item to remove
    var wishlistItem = user.Wishlist.FirstOrDefault(w => w.ProductId == productId);

    if (wishlistItem == null)
    {
        return BadRequest("Product not in wishlist"); // If the product is not in the wishlist
    }

    // Remove the product from wishlist
    user.Wishlist.Remove(wishlistItem); // Removes the product from the wishlist
    await _context.SaveChangesAsync(); // Saves changes to the database

    return Ok("Product removed from wishlist"); // Returns HTTP 200 on success
}
        // Helper method to check if a product exists
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id); // Checks if a product exists by ID
        }
    }
}