using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Gender { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsLocked { get; set; }

        public string? ImageUrl { get; set; }
    }
}