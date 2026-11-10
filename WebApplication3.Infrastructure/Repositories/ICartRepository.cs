using WebApplication3.Domain.Models;

namespace WebApplication3.Infrastructure.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetCartItemsAsync(int userId);

        Task<CartItem?> GetCartItemAsync(int userId, int productId);

        Task<Product?> GetProductByIdAsync(int productId);

        Task<bool> UserExistsAsync(int userId);

        Task AddCartItemAsync(CartItem cartItem);

        Task UpdateCartItemAsync(CartItem cartItem);

        Task RemoveCartItemAsync(CartItem cartItem);
    }
}