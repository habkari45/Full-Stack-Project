using Microsoft.EntityFrameworkCore;
using WebApplication3.Domain.Models;
using WebApplication3.Infrastructure.Data;

namespace WebApplication3.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            IEnumerable<Product> products =
                await _context.Products
                    .Include(x => x.Category)
                    .ToListAsync();

            return products;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        public Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);

            return _context.SaveChangesAsync();
        }

        public Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);

            return _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);

                await _context.SaveChangesAsync();
            }
        }
    }
}