using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using FlowerCommerceAPI.Models;
using FlowerCommerceAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;


namespace FlowerCommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWishlistService _wishlistService;
        private readonly IStringLocalizer<ProductsController> _localizer;

        public ProductsController(IProductService productService, IWishlistService wishlistService, IStringLocalizer<ProductsController> localizer)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _wishlistService = wishlistService ?? throw new ArgumentNullException(nameof(wishlistService));
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            if (products == null)
            {
                return NotFound(_localizer["Products_Not_Found"]);
            }

            return Ok(products);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            // Get the current culture (e.g., "en-US")
            var culture = CultureInfo.CurrentCulture.Name;

            // Fetch the product from the service (assuming _productService handles fetching with translations)
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Find the translation for the current culture or fallback to default language
            var productTranslation = product.Translations
                                            .FirstOrDefault(t => t.Language == culture) ??
                                     product.Translations.FirstOrDefault(t => t.Language == "en-US");

            // If a translation is found, use it. Otherwise, use localization service to fetch localized content
            if (productTranslation != null)
            {
                product.Name = productTranslation.Name;
                product.Description = productTranslation.Description;
            }
            else
            {
                // Fallback to localization service if no translation exists in the database
                var localizedName = _localizer["Product_Name"];
                var localizedDescription = _localizer["Product_Description"];

                product.Name = localizedName; // Replace with localized content from resource files
                product.Description = localizedDescription;
            }

            return Ok(product);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest(_localizer["Product_Data_Null"]);
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
                return BadRequest(_localizer["Product_ID_Mismatch"]);
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!await _productService.DeleteProductAsync(id))
            {
                return NotFound(_localizer["Product_Not_Found"]);
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
                return Unauthorized(_localizer["User_Not_Authenticated"]);
            }
            var userId = int.Parse(userIdClaim);

            if (!await _wishlistService.AddToWishlistAsync(userId, productId))
            {
                return BadRequest(_localizer["Product_Already_In_Wishlist"]);
            }

            return Ok(_localizer["Product_Added_To_Wishlist"]);
        }

        [Authorize]
        [HttpDelete("wishlist/remove/{productId}")]
        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            var userIdClaim = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(_localizer["User_Not_Authenticated"]);
            }
            var userId = int.Parse(userIdClaim);

            if (!await _wishlistService.RemoveFromWishlistAsync(userId, productId))
            {
                return BadRequest(_localizer["Product_Not_In_Wishlist"]);
            }

            return Ok(_localizer["Product_Removed_From_Wishlist"]);
        }
    }
}
