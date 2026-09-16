using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
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
        public ActionResult Delete(int Id)
        {
            var incident = _db.SecurityIncidents.Find(Id);

            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        [HttpPost]
        public ActionResult Delete(SecurityIncident incident)
        {
            _db.SecurityIncidents.Remove(incident);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}



