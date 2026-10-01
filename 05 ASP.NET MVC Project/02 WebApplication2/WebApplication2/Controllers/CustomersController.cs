using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly AppDbContext _db;

        public CustomersController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Customer> customers = _db.Customers.ToList();

            return View(customers);
        }

        // =====================
        // Create
        // =====================

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // =====================
        // Edit
        // =====================

        public IActionResult Edit(int id)
        {
            Customer? customer = _db.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Update(customer);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // =====================
        // Delete
        // =====================

        public IActionResult Delete(int id)
        {
            Customer? customer = _db.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            Customer? customer = _db.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            _db.Customers.Remove(customer);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}