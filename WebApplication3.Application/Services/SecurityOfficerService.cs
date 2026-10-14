using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Models;
using WebApplication3.Infrastructure.Repositories.Base;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.Application.Services
{
    public class SecurityOfficerService : ISecurityOfficerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityOfficerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET ALL
        public async Task<IEnumerable<SecurityOfficerDto>> GetAllSecurityOfficers()
        {
            var officers = await _unitOfWork.SecurityOfficerRepository
                .GetAllSecurityOfficersAsync();

            return officers.Select(officer => new SecurityOfficerDto
            {
                Id = officer.Id,
                Name = officer.Name,
                EmployeeNumber = officer.EmployeeNumber,
                Shift = officer.Shift,
                Phone = officer.Phone
            });
        }

        // GET BY ID
        public async Task<SecurityOfficerUpdateDto?> GetSecurityOfficerById(int id)
        {
            var officer = await _unitOfWork.SecurityOfficerRepository
                .GetSecurityOfficerByIdAsync(id);

            if (officer == null)
            {
                return null;
            }

            return new SecurityOfficerUpdateDto
            {
                Id = officer.Id,
                Name = officer.Name,
                EmployeeNumber = officer.EmployeeNumber,
                Shift = officer.Shift,
                Phone = officer.Phone
            };
        }

        // CREATE
        public async Task AddSecurityOfficer(SecurityOfficerCreateDto officerDTO)
        {
            var officer = new SecurityOfficer
            {
                Name = officerDTO.Name,
                EmployeeNumber = officerDTO.EmployeeNumber,
                Shift = officerDTO.Shift,
                Phone = officerDTO.Phone
            };

            await _unitOfWork.SecurityOfficerRepository
                .AddSecurityOfficerAsync(officer);
        }

        // UPDATE
        public async Task UpdateSecurityOfficer(SecurityOfficerUpdateDto officerDTO)
        {
            var officer = await _unitOfWork.SecurityOfficerRepository
                .GetSecurityOfficerByIdAsync(officerDTO.Id);

            if (officer == null)
            {
                throw new KeyNotFoundException("Security officer not found");
            }

            officer.Name = officerDTO.Name;
            officer.EmployeeNumber = officerDTO.EmployeeNumber;
            officer.Shift = officerDTO.Shift;
            officer.Phone = officerDTO.Phone;

            await _unitOfWork.SecurityOfficerRepository
                .UpdateSecurityOfficerAsync(officer);
        }

        // DELETE
        public async Task DeleteSecurityOfficer(int id)
        {
            var officer = await _unitOfWork.SecurityOfficerRepository
                .GetSecurityOfficerByIdAsync(id);

            if (officer == null)
            {
                throw new KeyNotFoundException("Security officer not found");
            }

            await _unitOfWork.SecurityOfficerRepository
                .DeleteSecurityOfficerAsync(id);
        }
    }
}
