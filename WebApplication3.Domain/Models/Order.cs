using System;
using System.Collections.Generic;

namespace WebApplication3.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> Items { get; set; }
            = new List<OrderItem>();
    }
}