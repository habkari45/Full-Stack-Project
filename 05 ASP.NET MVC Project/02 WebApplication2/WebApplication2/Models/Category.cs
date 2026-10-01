using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    [Index(nameof(Uuid), IsUnique = true)]
    public class Category
    {
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public string CategoryName { get; set; } = string.Empty;

        public string Description { get; set; }
    }
}
