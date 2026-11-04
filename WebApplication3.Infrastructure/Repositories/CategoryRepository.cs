using Microsoft.EntityFrameworkCore;
using WebApplication3.Domain.Models;
using WebApplication3.Infrastructure.Data;

namespace WebApplication3.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            IEnumerable<Category> categories =
                await _context.Categories.ToListAsync();

            return categories;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            return category;
        }

        public Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);

            return _context.SaveChangesAsync();
        }

        public Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);

            return _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
            {
                _context.Categories.Remove(category);

                await _context.SaveChangesAsync();
            }
        }
    }
}