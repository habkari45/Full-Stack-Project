namespace WebApplication3.Application.DTOs
{
    // DISPLAY CART ITEMS
    public class CartItemDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int UserId { get; set; }
    }

    // ADD TO CART
    public class AddToCartDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int UserId { get; set; }
    }
}