namespace WebApplication3.Domain.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}