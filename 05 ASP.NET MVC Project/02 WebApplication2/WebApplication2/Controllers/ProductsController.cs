using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products.ToList();

            return View(products);
        }

        // =====================
        // Create
        // =====================

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // =====================
        // Edit
        // =====================

        public IActionResult Edit(int id)
        {
            Product? product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(product);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // =====================
        // Delete
        // =====================

        public IActionResult Delete(int id)
        {
            Product? product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            Product? product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}