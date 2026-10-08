using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class SecurityOfficersController : Controller
    {
        private readonly ISecurityOfficerRepository _securityOfficerRepository;

        public SecurityOfficersController(
            ISecurityOfficerRepository securityOfficerRepository)
        {
            _securityOfficerRepository = securityOfficerRepository;
        }


        public async Task<IActionResult> Index()
        {
            var officers =
                await _securityOfficerRepository.GetAllSecurityOfficersAsync();

            return View(officers);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(SecurityOfficer officer)
        {
            if (ModelState.IsValid)
            {
                await _securityOfficerRepository
                    .AddSecurityOfficerAsync(officer);

                return RedirectToAction("Index");
            }

            return View(officer);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var officer =
                await _securityOfficerRepository
                    .GetSecurityOfficerByIdAsync(id);

            if (officer == null)
            {
                return NotFound();
            }

            return View(officer);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(SecurityOfficer officer)
        {
            if (ModelState.IsValid)
            {
                await _securityOfficerRepository
                    .UpdateSecurityOfficerAsync(officer);

                return RedirectToAction("Index");
            }

            return View(officer);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var officer =
                await _securityOfficerRepository
                    .GetSecurityOfficerByIdAsync(id);

            if (officer == null)
            {
                return NotFound();
            }

            return View(officer);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(SecurityOfficer officer)
        {
            await _securityOfficerRepository
                .DeleteSecurityOfficerAsync(officer.Id);

            return RedirectToAction("Index");
        }
    }
}