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

        public SecurityOfficersController(
            ISecurityOfficerService securityOfficerService)
        {
            _securityOfficerService = securityOfficerService;
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
            SecurityOfficerCreateDto officerDTO)
        {
            if (ModelState.IsValid)
            {
                await _securityOfficerService
                    .AddSecurityOfficer(officerDTO);

                return RedirectToAction("Index");
            }

            return View(officerDTO);
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
            SecurityOfficerUpdateDto officerDTO)
        {
            if (ModelState.IsValid)
            {
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

            return View(officerDTO);
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