using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityIncidentsController : ControllerBase
    {
        private readonly ISecurityIncidentService _securityIncidentService;

        public SecurityIncidentsController(
            ISecurityIncidentService securityIncidentService)
        {
            _securityIncidentService = securityIncidentService;
        }

        [HttpGet("GetAllSecurityIncidents")]
        public async Task<IActionResult> GetAllSecurityIncidents()
        {
            var incidents = await _securityIncidentService.GetAllSecurityIncidents();

            return Ok(new
            {
                message = "Security Incidents Retrieved Successfully",
                incidents = incidents
            });
        }
    }
}
