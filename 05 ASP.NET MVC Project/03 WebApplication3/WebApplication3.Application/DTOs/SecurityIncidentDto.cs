namespace WebApplication3.Application.DTOs
{
    public class SecurityIncidentDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string IncidentType { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime IncidentDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string SecurityOfficerName { get; set; } = string.Empty;
    }


    public class SecurityIncidentCreateDto
    {
        public string Title { get; set; } = string.Empty;

        public string IncidentType { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime IncidentDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int? SecurityOfficerId { get; set; }
    }


    public class SecurityIncidentUpdateDto : SecurityIncidentCreateDto
    {
        public int Id { get; set; }
    }
}
