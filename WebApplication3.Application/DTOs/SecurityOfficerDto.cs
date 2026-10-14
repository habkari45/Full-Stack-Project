namespace WebApplication3.Application.DTOs
{
    public class SecurityOfficerDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string EmployeeNumber { get; set; } = string.Empty;

        public string Shift { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
    }

    public class SecurityOfficerCreateDto
    {
        public string Name { get; set; } = string.Empty;

        public string EmployeeNumber { get; set; } = string.Empty;

        public string Shift { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
    }

    public class SecurityOfficerUpdateDto : SecurityOfficerCreateDto
    {
        public int Id { get; set; }
    }
}
