using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication3.Application.DTOs;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class SecurityIncidentsController : Controller
    {
        private readonly ISecurityIncidentService _securityIncidentService;

        public SecurityIncidentsController(
            ISecurityIncidentService securityIncidentService)
        {
            _securityIncidentService = securityIncidentService;
        }


        // GET ALL
        public async Task<IActionResult> Index()
        {
            var incidents =
                await _securityIncidentService.GetAllSecurityIncidents();

            return View(incidents);
        }


        // LOAD SECURITY OFFICERS
        private async Task loadSecurityOfficers()
        {
            var securityOfficers =
                await _securityIncidentService.GetAllSecurityOfficers();

            ViewBag.SecurityOfficers =
                new SelectList(securityOfficers, "Id", "Name");
        }


        // CREATE GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await loadSecurityOfficers();

            return View();
        }


        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(
            SecurityIncidentCreateDto incidentDTO)
        {
            if (ModelState.IsValid)
            {
                await _securityIncidentService
                    .AddSecurityIncident(incidentDTO);

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(
                "",
                "Please fill in all required fields.");

            await loadSecurityOfficers();

            return View(incidentDTO);
        }


        // EDIT GET
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await loadSecurityOfficers();

            var incidentDTO =
                await _securityIncidentService
                    .GetSecurityIncidentById(id.Value);

            if (incidentDTO == null)
            {
                return NotFound();
            }

            return View(incidentDTO);
        }


        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(
            SecurityIncidentUpdateDto incidentDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _securityIncidentService
                        .UpdateSecurityIncident(incidentDTO);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(
                "",
                "Please fill all the required fields.");

            await loadSecurityOfficers();

            return View(incidentDTO);
        }


        // DELETE GET
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidentDTO =
                await _securityIncidentService
                    .GetSecurityIncidentById(id.Value);

            if (incidentDTO == null)
            {
                return NotFound();
            }

            return View(incidentDTO);
        }


        // DELETE POST
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _securityIncidentService
                    .DeleteSecurityIncident(id.Value);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}