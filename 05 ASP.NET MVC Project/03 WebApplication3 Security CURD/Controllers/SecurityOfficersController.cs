using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class SecurityOfficersController : Controller
    {
        private readonly AppDbContext _db;

        public SecurityOfficersController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<SecurityOfficer> officers = _db.SecurityOfficers.ToList();

            return View(officers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SecurityOfficer officer)
        {
            _db.SecurityOfficers.Add(officer);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            SecurityOfficer officer = _db.SecurityOfficers.Find(id);

            return View(officer);
        }

        [HttpPost]
        public IActionResult Edit(SecurityOfficer officer)
        {
            _db.SecurityOfficers.Update(officer);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            SecurityOfficer officer = _db.SecurityOfficers.Find(id);

            return View(officer);
        }

        [HttpPost]
        public IActionResult Delete(SecurityOfficer officer)
        {
            _db.SecurityOfficers.Remove(officer);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
