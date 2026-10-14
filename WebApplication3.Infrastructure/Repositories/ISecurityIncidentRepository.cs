using WebApplication3.Domain.Models;

namespace WebApplication3.Infrastructure.Repositories
{
    public interface ISecurityIncidentRepository
    {
        Task<IEnumerable<SecurityIncident>> GetAllSecurityIncidentsAsync();

        Task<SecurityIncident> GetSecurityIncidentByIdAsync(int id);

        Task AddSecurityIncidentAsync(SecurityIncident securityIncident);

        Task UpdateSecurityIncidentAsync(SecurityIncident securityIncident);

        Task DeleteSecurityIncidentAsync(int id);
    }
}
