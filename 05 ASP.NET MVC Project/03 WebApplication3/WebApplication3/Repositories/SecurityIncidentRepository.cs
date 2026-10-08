using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class SecurityIncidentRepository : ISecurityIncidentRepository
    {
        private readonly AppDbContext _context;

        public SecurityIncidentRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<SecurityIncident>> GetAllSecurityIncidentsAsync()
        {
            IEnumerable<SecurityIncident> incidents =
                await _context.SecurityIncidents
                .Include(i => i.SecurityOfficer)
                .ToListAsync();

            return incidents;
        }


        public async Task<SecurityIncident> GetSecurityIncidentByIdAsync(int id)
        {
            var incident = await _context.SecurityIncidents
                .Include(i => i.SecurityOfficer)
                .FirstOrDefaultAsync(i => i.Id == id);

            return incident;
        }


        public Task AddSecurityIncidentAsync(SecurityIncident securityIncident)
        {
            _context.SecurityIncidents.Add(securityIncident);

            return _context.SaveChangesAsync();
        }


        public Task UpdateSecurityIncidentAsync(SecurityIncident securityIncident)
        {
            _context.SecurityIncidents.Update(securityIncident);

            return _context.SaveChangesAsync();
        }


        public Task DeleteSecurityIncidentAsync(int id)
        {
            var incident = _context.SecurityIncidents.Find(id);

            if (incident != null)
            {
                _context.SecurityIncidents.Remove(incident);

                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
    }
}
