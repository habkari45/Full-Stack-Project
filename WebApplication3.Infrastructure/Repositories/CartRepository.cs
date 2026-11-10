using Microsoft.EntityFrameworkCore;
using WebApplication3.Domain.Models;
using WebApplication3.Infrastructure.Data;

namespace WebApplication3.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL CART ITEMS
        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(int userId)
        {
            return await _context.CartItems
                .Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        // GET CART ITEM
        public async Task<CartItem?> GetCartItemAsync(
            int userId, int productId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.ProductId == productId);
        }

        // GET PRODUCT
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products
                .FindAsync(productId);
        }

        // CHECK USER
        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users
                .AnyAsync(x => x.Id == userId);
        }

        // ADD
        public async Task AddCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);

            await _context.SaveChangesAsync();
        }

        // UPDATE
        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);

            await _context.SaveChangesAsync();
        }

        // DELETE
        public async Task RemoveCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);

            await _context.SaveChangesAsync();
        }
    }
}