using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _db;

        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories.ToList();

            return View(categories);
        }

        // =====================
        // Create
        // =====================

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // =====================
        // Edit
        // =====================

        public IActionResult Edit(int id)
        {
            Category? category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // =====================
        // Delete
        // =====================

        public IActionResult Delete(int id)
        {
            Category? category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            Category? category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}