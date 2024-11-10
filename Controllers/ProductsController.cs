using Microsoft.AspNetCore.Mvc;
using FlowerCommerceAPI.Data;
using FlowerCommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerCommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Products (Fetch all products)
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        // GET: api/Products/5 (Fetch a single product by ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Products (Create a new product)
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
    }
}
