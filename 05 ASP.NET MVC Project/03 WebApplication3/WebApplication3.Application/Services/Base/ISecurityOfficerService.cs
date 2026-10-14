using WebApplication3.Application.DTOs;

namespace WebApplication3.Application.Services.Base
{
    public interface ISecurityOfficerService
    {
        Task<IEnumerable<SecurityOfficerDto>> GetAllSecurityOfficers();

        Task<SecurityOfficerUpdateDto?> GetSecurityOfficerById(int id);

        Task AddSecurityOfficer(SecurityOfficerCreateDto officerDTO);

        Task UpdateSecurityOfficer(SecurityOfficerUpdateDto officerDTO);

        Task DeleteSecurityOfficer(int id);
    }
}