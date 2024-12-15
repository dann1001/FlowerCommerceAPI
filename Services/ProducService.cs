using FlowerCommerceAPI.Models; // for the Product class
using FlowerCommerceAPI.Data;   // for the AppDbContext class
using Microsoft.EntityFrameworkCore;

namespace FlowerCommerceAPI.Services
{
    /// <summary>
    /// Interface for product-related operations in the service layer.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products from the database asynchronously.
        /// </summary>
        /// <returns>A collection of all products.</returns>
        Task<IEnumerable<Product>> GetProductsAsync();

        /// <summary>
        /// Retrieves a specific product by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The requested product.</returns>
        Task<Product> GetProductByIdAsync(int id);

        /// <summary>
        /// Creates a new product asynchronously.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        Task<Product> CreateProductAsync(Product product);

        /// <summary>
        /// Updates an existing product asynchronously.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        Task<bool> UpdateProductAsync(int id, Product product);

        /// <summary>
        /// Deletes a product by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>True if the product was successfully deleted, false otherwise.</returns>
        Task<bool> DeleteProductAsync(int id);
    }

    /// <summary>
    /// Service for handling product-related operations.
    /// Implements <see cref="IProductService"/> to interact with the database.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing product data.</param>
        public ProductService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retrieves all products from the database asynchronously.
        /// </summary>
        /// <returns>A collection of all products.</returns>
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific product by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The requested product if found, otherwise null.</returns>
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        /// <summary>
        /// Creates a new product asynchronously.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Updates an existing product asynchronously.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        /// <returns>True if the update was successful, false if IDs do not match or the product is not found.</returns>
        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            if (id != product.Id)
            {
                return false; // IDs do not match
            }

            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == id))
                {
                    return false; // Product not found
                }
                throw;
            }
            return true;
        }

        /// <summary>
        /// Deletes a product by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>True if the product was successfully deleted, false if the product is not found.</returns>
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false; // Product not found
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
