using WebApplication3.Application.DTOs;

namespace WebApplication3.Application.Services.Base
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemDto>> GetCartItems(int userId);

        Task AddToCart(AddToCartDto cartDto);

        Task RemoveFromCart(int userId, int productId);
    }
}