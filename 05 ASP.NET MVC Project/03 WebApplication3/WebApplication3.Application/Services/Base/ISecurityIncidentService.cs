using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Models;

namespace WebApplication3.Application.Services.Base
{
    public interface ISecurityIncidentService
    {
        Task<IEnumerable<SecurityIncidentDto>> GetAllSecurityIncidents();

        Task<SecurityIncidentUpdateDto?> GetSecurityIncidentById(int id);

        Task<IEnumerable<SecurityOfficer>> GetAllSecurityOfficers();

        Task AddSecurityIncident(SecurityIncidentCreateDto incidentDTO);

        Task UpdateSecurityIncident(SecurityIncidentUpdateDto incidentDTO);

        Task DeleteSecurityIncident(int id);
    }
}