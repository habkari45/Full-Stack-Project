using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.DTOs;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET CART ITEMS
        [HttpGet("GetCartItems/{userId}")]
        public async Task<IActionResult> GetCartItems(int userId)
        {
            var cartItems = await _cartService
                .GetCartItems(userId);

            return Ok(cartItems);
        }

        // ADD TO CART
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(
            [FromBody] AddToCartDto request)
        {
            try
            {
                await _cartService.AddToCart(request);

                return Ok(new
                {
                    message = "Item added to cart"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }

        // REMOVE FROM CART
        [HttpDelete("RemoveFromCart/{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromCart(


            int userId,
            int productId)
        {
            try
            {
                await _cartService.RemoveFromCart(userId, productId);

                return Ok(new
                {
                    message = "Item removed from cart"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }
    }
}