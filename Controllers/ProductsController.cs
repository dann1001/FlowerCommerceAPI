using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlowerCommerceAPI.Models;
using FlowerCommerceAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowerCommerceAPI.Controllers
{
    /// <summary>
    /// Controller responsible for handling product-related API requests.
    /// Provides endpoints for retrieving, creating, updating, and deleting products.
    /// Also allows users to add or remove products from their wishlist.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWishlistService _wishlistService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productService">Service for handling product-related operations.</param>
        /// <param name="wishlistService">Service for handling wishlist-related operations.</param>
        public ProductsController(IProductService productService, IWishlistService wishlistService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _wishlistService = wishlistService ?? throw new ArgumentNullException(nameof(wishlistService));
        }

        /// <summary>
        /// Gets all products in the system (Public Access).
        /// </summary>
        /// <returns>A list of all products.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            if (products == null)
            {
                return NotFound("Products not found.");
            }

            return Ok(products);
        }

        /// <summary>
        /// Gets a specific product by its ID (Public Access).
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The requested product if found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Creates a new product (Admin Only).
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product with its ID.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null");
            }

            var createdProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        /// <summary>
        /// Updates an existing product by its ID (Admin Only).
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        /// <returns>A status of the update operation.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (!await _productService.UpdateProductAsync(id, product))
            {
                return BadRequest("Product ID mismatch or product not found");
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a product by its ID (Admin Only).
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A status of the delete operation.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!await _productService.DeleteProductAsync(id))
            {
                return NotFound("Product not found");
            }

            return NoContent();
        }

        /// <summary>
        /// Adds a product to the authenticated user's wishlist.
        /// </summary>
        /// <param name="productId">The ID of the product to add to the wishlist.</param>
        /// <returns>A status of the add operation.</returns>
        [Authorize]
        [HttpPost("wishlist/add/{productId}")]
        public async Task<IActionResult> AddToWishlist(int productId)
        {
            var userIdClaim = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User not authenticated or ID claim missing.");
            }
            var userId = int.Parse(userIdClaim);

            if (!await _wishlistService.AddToWishlistAsync(userId, productId))
            {
                return BadRequest("Product already in wishlist or not found.");
            }

            return Ok("Product added to wishlist.");
        }

        /// <summary>
        /// Removes a product from the authenticated user's wishlist.
        /// </summary>
        /// <param name="productId">The ID of the product to remove from the wishlist.</param>
        /// <returns>A status of the remove operation.</returns>
        [Authorize]
        [HttpDelete("wishlist/remove/{productId}")]
        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            var userIdClaim = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User not authenticated or ID claim missing.");
            }
            var userId = int.Parse(userIdClaim);

            if (!await _wishlistService.RemoveFromWishlistAsync(userId, productId))
            {
                return BadRequest("Product not in wishlist or not found.");
            }

            return Ok("Product removed from wishlist");
        }
    }
}
