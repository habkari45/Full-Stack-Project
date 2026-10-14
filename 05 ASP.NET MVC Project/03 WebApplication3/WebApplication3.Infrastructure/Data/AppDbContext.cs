using Microsoft.EntityFrameworkCore;
using WebApplication3.Domain.Models;

namespace WebApplication3.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<SecurityIncident> SecurityIncidents { get; set; }
        public DbSet<SecurityOfficer> SecurityOfficers { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
