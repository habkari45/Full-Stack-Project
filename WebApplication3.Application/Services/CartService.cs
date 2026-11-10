using WebApplication3.Application.DTOs;
using WebApplication3.Application.Services.Base;
using WebApplication3.Domain.Models;
using WebApplication3.Infrastructure.Repositories.Base;

namespace WebApplication3.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET CART ITEMS
        public async Task<IEnumerable<CartItemDto>> GetCartItems(int userId)
        {
            var cartItems = await _unitOfWork.CartRepository
                .GetCartItemsAsync(userId);

            return cartItems.Select(item => new CartItemDto
            {
                Id = item.Id,
                ProductId = item.ProductId,
                ProductName = item.Product.Name,
                Quantity = item.Quantity,
                ImageUrl = item.Product.ImageUrl,
                Price = item.Price,
                UserId = item.UserId
            });
        }

        // ADD TO CART
        public async Task AddToCart(AddToCartDto cartDto)
        {
            if (cartDto.Quantity <= 0)
            {
                throw new ArgumentException(
                    "Quantity must be greater than zero");
            }

            var product = await _unitOfWork.CartRepository
                .GetProductByIdAsync(cartDto.ProductId);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            var userExists = await _unitOfWork.CartRepository
                .UserExistsAsync(cartDto.UserId);

            if (!userExists)
            {
                throw new KeyNotFoundException("User not found");
            }

            var cartItem = await _unitOfWork.CartRepository
                .GetCartItemAsync(cartDto.UserId, cartDto.ProductId);

            if (cartItem == null)
            {
                var newCartItem = new CartItem
                {
                    UserId = cartDto.UserId,
                    ProductId = cartDto.ProductId,
                    Quantity = cartDto.Quantity,
                    Price = product.Price
                };

                await _unitOfWork.CartRepository
                    .AddCartItemAsync(newCartItem);
            }
            else
            {
                cartItem.Quantity += cartDto.Quantity;

                await _unitOfWork.CartRepository
                    .UpdateCartItemAsync(cartItem);
            }
        }

        // REMOVE FROM CART
        public async Task RemoveFromCart(int userId, int productId)
        {
            var cartItem = await _unitOfWork.CartRepository
                .GetCartItemAsync(userId, productId);

            if (cartItem == null)
            {
                throw new KeyNotFoundException(
                    "Item not found in cart");
            }

            await _unitOfWork.CartRepository
                .RemoveCartItemAsync(cartItem);
        }
    }
}