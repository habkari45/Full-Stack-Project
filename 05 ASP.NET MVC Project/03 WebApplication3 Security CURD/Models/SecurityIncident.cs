using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class SecurityIncident
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Incident Title")]
        [Required(ErrorMessage = "Incident Title is required.")]
        public string Title { get; set; }

        [DisplayName("Incident Type")]
        [Required(ErrorMessage = "Incident Type is required.")]
        public string IncidentType { get; set; }

        [DisplayName("Location")]
        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [DisplayName("Incident Date")]
        [Required(ErrorMessage = "Incident Date is required.")]
        public DateTime IncidentDate { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}
