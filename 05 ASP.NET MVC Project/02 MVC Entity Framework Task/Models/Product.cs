using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}

