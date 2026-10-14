

namespace WebApplication3.Domain.Models
{
    public class SecurityOfficer
    {
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string EmployeeNumber { get; set; }

        public string Shift { get; set; }

        public string Phone { get; set; }

        public ICollection<SecurityIncident>? SecurityIncidents { get; set; }
    }
}