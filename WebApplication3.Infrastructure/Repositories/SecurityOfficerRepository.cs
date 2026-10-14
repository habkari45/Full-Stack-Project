using Microsoft.EntityFrameworkCore;
using WebApplication3.Infrastructure.Data;
using WebApplication3.Domain.Models;

namespace WebApplication3.Infrastructure.Repositories
{
    public class SecurityOfficerRepository : ISecurityOfficerRepository
    {
        private readonly AppDbContext _context;

        public SecurityOfficerRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<SecurityOfficer>> GetAllSecurityOfficersAsync()
        {
            IEnumerable<SecurityOfficer> securityOfficers =
                await _context.SecurityOfficers.ToListAsync();

            return securityOfficers;
        }


        public async Task<SecurityOfficer> GetSecurityOfficerByIdAsync(int id)
        {
            var securityOfficer =
                await _context.SecurityOfficers.FindAsync(id);

            return securityOfficer;
        }


        public Task AddSecurityOfficerAsync(SecurityOfficer securityOfficer)
        {
            _context.SecurityOfficers.Add(securityOfficer);

            return _context.SaveChangesAsync();
        }


        public Task UpdateSecurityOfficerAsync(SecurityOfficer securityOfficer)
        {
            _context.SecurityOfficers.Update(securityOfficer);

            return _context.SaveChangesAsync();
        }


        public Task DeleteSecurityOfficerAsync(int id)
        {
            var securityOfficer =
                _context.SecurityOfficers.Find(id);

            if (securityOfficer != null)
            {
                _context.SecurityOfficers.Remove(securityOfficer);

                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
    }
}
