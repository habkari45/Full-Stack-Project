using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    [Index(nameof(Uuid), IsUnique = true)]
    public class Customer
    {
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
    }
}