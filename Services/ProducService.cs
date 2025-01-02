using FlowerCommerceAPI.Models;
using FlowerCommerceAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FlowerCommerceAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(int id, Product product);
        Task<bool> DeleteProductAsync(int id);

        void AddProduct(Product product);
        Task SaveAsync();
    }

    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                                 .Include(p => p.WishlistedBy) // Assuming there's a navigation property `WishlistedBy`.
                                 .Include(p => p.Translations)    // Include Translations
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            if (product.Translations != null)
            {
                foreach (var translation in product.Translations)
                {
                    _context.Entry(translation).State = EntityState.Added;
                }
            }
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            if (id != product.Id)
            {
                return false;
            }

            _context.Entry(product).State = EntityState.Modified;
            // Handle translations
            var existingTranslations = _context.ProductTranslations.Where(t => t.ProductId == id);
            _context.ProductTranslations.RemoveRange(existingTranslations);

            if (product.Translations != null)
            {
                foreach (var translation in product.Translations)
                {
                    _context.ProductTranslations.Add(translation);
                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == id))
                {
                    return false;
                }
                throw;
            }
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
