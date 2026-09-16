using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<SecurityIncident> SecurityIncidents { get; set; }
        public DbSet<SecurityOfficer> SecurityOfficers { get; set; }

    }
}
