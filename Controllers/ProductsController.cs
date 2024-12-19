using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlowerCommerceAPI.Models;
using FlowerCommerceAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowerCommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWishlistService _wishlistService;

        public ProductsController(IProductService productService, IWishlistService wishlistService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _wishlistService = wishlistService ?? throw new ArgumentNullException(nameof(wishlistService));
        }

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
