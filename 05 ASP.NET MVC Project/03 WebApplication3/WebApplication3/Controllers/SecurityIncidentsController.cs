using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class SecurityIncidentsController : Controller
    {
        //DI
        private readonly AppDbContext _db;

        public SecurityIncidentsController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            IEnumerable<SecurityIncident> incidents = _db.SecurityIncidents.ToList();
            return View(incidents);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SecurityIncident incident)
        {
            if (ModelState.IsValid)
            {
                _db.SecurityIncidents.Add(incident);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill in all required fields.");
            return View(incident);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var incident = _db.SecurityIncidents.Find(Id);

            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        [HttpPost]
        public ActionResult Edit(SecurityIncident incident)
        {
            if (ModelState.IsValid)
            {
                _db.SecurityIncidents.Update(incident);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(incident);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            SecurityIncident? incident = _db.SecurityIncidents.Find(id);

            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(SecurityIncident incident)
        {
            SecurityIncident? incidentFromDb = _db.SecurityIncidents.Find(incident.Id);

            if (incidentFromDb == null)
            {
                return NotFound();
            }

            _db.SecurityIncidents.Remove(incidentFromDb);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}



