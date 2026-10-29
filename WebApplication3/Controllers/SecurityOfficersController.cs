using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.DTOs;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class SecurityOfficersController : Controller
    {
        private readonly ISecurityOfficerService _securityOfficerService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SecurityOfficersController(
            ISecurityOfficerService securityOfficerService,
            IWebHostEnvironment webHostEnvironment)
        {
            _securityOfficerService = securityOfficerService;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET ALL
        public async Task<IActionResult> Index()
        {
            var officers =
                await _securityOfficerService.GetAllSecurityOfficers();

            return View(officers);
        }


        // CREATE GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(
            SecurityOfficerCreateDto officerDTO,
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
                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("imageFile",
                        "Only JPG, JPEG and PNG images are allowed.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(officerDTO);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var webRootPath = _webHostEnvironment.WebRootPath
                    ?? Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");

                var folderPath = Path.Combine(webRootPath, "images", "officers");

                Directory.CreateDirectory(folderPath);

                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

                var fileName = $"{Guid.NewGuid()}{extension}";

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await imageFile.CopyToAsync(stream);
                }

                officerDTO.ImageUrl = $"/images/officers/{fileName}";
            }

            await _securityOfficerService.AddSecurityOfficer(officerDTO);

            return RedirectToAction("Index");
        }



        // EDIT GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var officerDTO =
                await _securityOfficerService
                    .GetSecurityOfficerById(id);

            if (officerDTO == null)
            {
                return NotFound();
            }

            return View(officerDTO);
        }


        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(
            SecurityOfficerUpdateDto officerDTO,
            IFormFile? imageFile)
        {
            // Get the current officer
            var existingOfficer = await _securityOfficerService
                .GetSecurityOfficerById(officerDTO.Id);

            if (existingOfficer == null)
            {
                return NotFound();
            }

            // Keep the current image
            officerDTO.ImageUrl = existingOfficer.ImageUrl;

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
                return View(officerDTO);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var webRootPath = _webHostEnvironment.WebRootPath
                    ?? Path.Combine(
                        _webHostEnvironment.ContentRootPath,
                        "wwwroot");

                var folderPath = Path.Combine(
                    webRootPath, "images", "officers");

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

                officerDTO.ImageUrl = $"/images/officers/{fileName}";
            }

            try
            {
                await _securityOfficerService
                    .UpdateSecurityOfficer(officerDTO);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }


        // DELETE GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var officerDTO =
                await _securityOfficerService
                    .GetSecurityOfficerById(id);

            if (officerDTO == null)
            {
                return NotFound();
            }

            return View(officerDTO);
        }


        // DELETE POST
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                await _securityOfficerService
                    .DeleteSecurityOfficer(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}