using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Models;
using WebApplication3.Infrastructure.Repositories.Base;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.Application.Services
{
    public class SecurityIncidentService : ISecurityIncidentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityIncidentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET ALL
        public async Task<IEnumerable<SecurityIncidentDto>> GetAllSecurityIncidents()
        {
            var incidents = await _unitOfWork.SecurityIncidentRepository
                .GetAllSecurityIncidentsAsync();

            return incidents.Select(incident => new SecurityIncidentDto
            {
                Id = incident.Id,
                Title = incident.Title,
                IncidentType = incident.IncidentType,
                Location = incident.Location,
                IncidentDate = incident.IncidentDate,
                Status = incident.Status,
                Description = incident.Description,
                SecurityOfficerName = incident.SecurityOfficer?.Name ?? string.Empty
            });
        }

        // GET BY ID
        public async Task<SecurityIncidentUpdateDto?> GetSecurityIncidentById(int id)
        {
            var incident = await _unitOfWork.SecurityIncidentRepository
                .GetSecurityIncidentByIdAsync(id);

            if (incident == null)
            {
                return null;
            }

            return new SecurityIncidentUpdateDto
            {
                Id = incident.Id,
                Title = incident.Title,
                IncidentType = incident.IncidentType,
                Location = incident.Location,
                IncidentDate = incident.IncidentDate,
                Status = incident.Status,
                Description = incident.Description,
                SecurityOfficerId = incident.SecurityOfficerId
            };
        }

        // GET SECURITY OFFICERS
        public async Task<IEnumerable<SecurityOfficer>> GetAllSecurityOfficers()
        {
            return await _unitOfWork.SecurityOfficerRepository
                .GetAllSecurityOfficersAsync();
        }

        // CREATE
        public async Task AddSecurityIncident(SecurityIncidentCreateDto incidentDTO)
        {
            var incident = new SecurityIncident
            {
                Title = incidentDTO.Title,
                IncidentType = incidentDTO.IncidentType,
                Location = incidentDTO.Location,
                IncidentDate = incidentDTO.IncidentDate,
                Status = incidentDTO.Status,
                Description = incidentDTO.Description,
                SecurityOfficerId = incidentDTO.SecurityOfficerId
            };

            await _unitOfWork.SecurityIncidentRepository
                .AddSecurityIncidentAsync(incident);
        }

        // UPDATE
        public async Task UpdateSecurityIncident(SecurityIncidentUpdateDto incidentDTO)
        {
            var incident = await _unitOfWork.SecurityIncidentRepository
                .GetSecurityIncidentByIdAsync(incidentDTO.Id);

            if (incident == null)
            {
                throw new KeyNotFoundException("Security incident not found");
            }

            incident.Title = incidentDTO.Title;
            incident.IncidentType = incidentDTO.IncidentType;
            incident.Location = incidentDTO.Location;
            incident.IncidentDate = incidentDTO.IncidentDate;
            incident.Status = incidentDTO.Status;
            incident.Description = incidentDTO.Description;
            incident.SecurityOfficerId = incidentDTO.SecurityOfficerId;

            await _unitOfWork.SecurityIncidentRepository
                .UpdateSecurityIncidentAsync(incident);
        }

        // DELETE
        public async Task DeleteSecurityIncident(int id)
        {
            var incident = await _unitOfWork.SecurityIncidentRepository
                .GetSecurityIncidentByIdAsync(id);

            if (incident == null)
            {
                throw new KeyNotFoundException("Security incident not found");
            }

            await _unitOfWork.SecurityIncidentRepository
                .DeleteSecurityIncidentAsync(id);
        }
    }
}
