using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityOfficersController : ControllerBase
    {
        private readonly ISecurityOfficerService _securityOfficerService;

        public SecurityOfficersController(ISecurityOfficerService securityOfficerService)
        {
            _securityOfficerService = securityOfficerService;
        }

        [HttpGet("GetAllSecurityOfficers")]
        public async Task<IActionResult> GetAllSecurityOfficers()
        {
            var officers = await _securityOfficerService.GetAllSecurityOfficers();

            return Ok(new
            {
                message = "Security Officers Retrieved Successfully",
                officers = officers
            });
        }
    }
}
