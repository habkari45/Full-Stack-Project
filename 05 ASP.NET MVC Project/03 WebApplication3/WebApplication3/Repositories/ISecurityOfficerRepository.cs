using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public interface ISecurityOfficerRepository
    {
        Task<IEnumerable<SecurityOfficer>> GetAllSecurityOfficersAsync();

        Task<SecurityOfficer> GetSecurityOfficerByIdAsync(int id);

        Task AddSecurityOfficerAsync(SecurityOfficer securityOfficer);

        Task UpdateSecurityOfficerAsync(SecurityOfficer securityOfficer);

        Task DeleteSecurityOfficerAsync(int id);
    }
}
