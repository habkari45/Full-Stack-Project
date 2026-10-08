using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication3.Dtos;
using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class SecurityIncidentsController : Controller
    {
        private readonly ISecurityIncidentRepository _securityIncidentRepository;
        private readonly ISecurityOfficerRepository _securityOfficerRepository;

        public SecurityIncidentsController(
            ISecurityIncidentRepository securityIncidentRepository,
            ISecurityOfficerRepository securityOfficerRepository)
        {
            _securityIncidentRepository = securityIncidentRepository;
            _securityOfficerRepository = securityOfficerRepository;
        }


        public async Task<IActionResult> Index()
        {
            var securityIncidents =
                await _securityIncidentRepository.GetAllSecurityIncidentsAsync();

            IEnumerable<SecurityIncidentDto> incidents =
                securityIncidents.Select(i => new SecurityIncidentDto
                {
                    //Mapping SecurityIncident to SecurityIncidentDto
                    Id = i.Id,
                    Title = i.Title,
                    IncidentType = i.IncidentType,
                    Location = i.Location,
                    IncidentDate = i.IncidentDate,
                    Status = i.Status,
                    Description = i.Description,
                    SecurityOfficerName =
                        i.SecurityOfficer != null
                        ? i.SecurityOfficer.Name
                        : ""
                }).ToList();

            return View(incidents);
        }


        private async Task loadSecurityOfficers()
        {
            var securityOfficers =
                await _securityOfficerRepository.GetAllSecurityOfficersAsync();

            ViewBag.SecurityOfficers =
                new SelectList(securityOfficers, "Id", "Name");
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await loadSecurityOfficers();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            SecurityIncidentCreateDto incidentDTO)
        {
            if (ModelState.IsValid)
            {
                //Mapping SecurityIncidentCreateDto to SecurityIncident
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

                await _securityIncidentRepository
                    .AddSecurityIncidentAsync(incident);

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(
                "",
                "Please fill in all required fields.");

            await loadSecurityOfficers();

            return View(incidentDTO);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await loadSecurityOfficers();

            var incident =
                await _securityIncidentRepository
                    .GetSecurityIncidentByIdAsync(id.Value);

            if (incident == null)
            {
                return NotFound();
            }

            var incidentDTO = new SecurityIncidentUpdateDto
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

            return View(incidentDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(
            SecurityIncidentUpdateDto incidentDTO)
        {
            if (ModelState.IsValid)
            {
                var incident =
                    await _securityIncidentRepository
                        .GetSecurityIncidentByIdAsync(incidentDTO.Id);

                if (incident == null)
                {
                    return NotFound();
                }

                //Mapping SecurityIncidentUpdateDto to SecurityIncident
                incident.Title = incidentDTO.Title;
                incident.IncidentType = incidentDTO.IncidentType;
                incident.Location = incidentDTO.Location;
                incident.IncidentDate = incidentDTO.IncidentDate;
                incident.Status = incidentDTO.Status;
                incident.Description = incidentDTO.Description;
                incident.SecurityOfficerId =
                    incidentDTO.SecurityOfficerId;

                await _securityIncidentRepository
                    .UpdateSecurityIncidentAsync(incident);

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(
                "",
                "Please fill all the required fields.");

            await loadSecurityOfficers();

            return View(incidentDTO);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident =
                await _securityIncidentRepository
                    .GetSecurityIncidentByIdAsync(id.Value);

            if (incident == null)
            {
                return NotFound();
            }

            var incidentDTO = new SecurityIncidentUpdateDto
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

            return View(incidentDTO);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident =
                await _securityIncidentRepository
                    .GetSecurityIncidentByIdAsync(id.Value);

            if (incident == null)
            {
                return NotFound();
            }

            await _securityIncidentRepository
                .DeleteSecurityIncidentAsync(incident.Id);

            return RedirectToAction("Index");
        }
    }
}