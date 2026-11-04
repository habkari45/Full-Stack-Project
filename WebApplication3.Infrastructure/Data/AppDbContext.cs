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

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SecurityOfficer>()
                .HasIndex(x => x.Uuid)
                .IsUnique();

            modelBuilder.Entity<SecurityIncident>()
                .HasIndex(x => x.Uuid)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}