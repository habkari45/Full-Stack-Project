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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SecurityIncidentsController(
         ISecurityIncidentService securityIncidentService,
          IWebHostEnvironment webHostEnvironment)
        {
            _securityIncidentService = securityIncidentService;
            _webHostEnvironment = webHostEnvironment;
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
            SecurityIncidentCreateDto incidentDTO,
            IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Validate image size (5 MB)
                if (imageFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("imageFile",
                        "Image size must not exceed 5 MB.");
                }

                // Validate image extension
                var extension = Path.GetExtension(imageFile.FileName)
                    .ToLowerInvariant();

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("imageFile",
                        "Only JPG, JPEG and PNG images are allowed.");
                }
            }

            if (!ModelState.IsValid)
            {
                await loadSecurityOfficers();

                return View(incidentDTO);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var webRootPath = _webHostEnvironment.WebRootPath
                    ?? Path.Combine(
                        _webHostEnvironment.ContentRootPath,
                        "wwwroot");

                var folderPath = Path.Combine(
                    webRootPath, "images", "incidents");

                Directory.CreateDirectory(folderPath);

                var extension = Path.GetExtension(imageFile.FileName)
                    .ToLowerInvariant();

                var fileName = $"{Guid.NewGuid()}{extension}";

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(
                    filePath, FileMode.CreateNew))
                {
                    await imageFile.CopyToAsync(stream);
                }

                incidentDTO.ImageUrl = $"/images/incidents/{fileName}";
            }

            await _securityIncidentService
                .AddSecurityIncident(incidentDTO);

            return RedirectToAction("Index");
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
            SecurityIncidentUpdateDto incidentDTO,
            IFormFile? imageFile)
        {
            // Get current incident
            var existingIncident = await _securityIncidentService
                .GetSecurityIncidentById(incidentDTO.Id);

            if (existingIncident == null)
            {
                return NotFound();
            }

            // Keep the current image
            incidentDTO.ImageUrl = existingIncident.ImageUrl;

            if (imageFile != null && imageFile.Length > 0)
            {
                // Validate image size (5 MB)
                if (imageFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("imageFile",
                        "Image size must not exceed 5 MB.");
                }

                // Validate image extension
                var extension = Path.GetExtension(imageFile.FileName)
                    .ToLowerInvariant();

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("imageFile",
                        "Only JPG, JPEG and PNG images are allowed.");
                }
            }

            if (!ModelState.IsValid)
            {
                await loadSecurityOfficers();

                return View(incidentDTO);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var webRootPath = _webHostEnvironment.WebRootPath
                    ?? Path.Combine(
                        _webHostEnvironment.ContentRootPath,
                        "wwwroot");

                var folderPath = Path.Combine(
                    webRootPath, "images", "incidents");

                Directory.CreateDirectory(folderPath);

                var extension = Path.GetExtension(imageFile.FileName)
                    .ToLowerInvariant();

                var fileName = $"{Guid.NewGuid()}{extension}";

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(
                    filePath, FileMode.CreateNew))
                {
                    await imageFile.CopyToAsync(stream);
                }

                incidentDTO.ImageUrl = $"/images/incidents/{fileName}";
            }

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