
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;



namespace WebApplication2.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }


        // =========================
        // Index
        // =========================
        public IActionResult Index()
        {
            var users = _db.Users.ToList();

            return View(users);
        }


        // =========================
        // Details
        // =========================
        public IActionResult Details(int id)
        {
            var user = _db.Users.Find(id);

            if (user == null)
                return NotFound();

            return View(user);
        }


        // =========================
        // Create GET
        // =========================
        public IActionResult Create()
        {
            return View();
        }


        // =========================
        // Create POST
        // =========================
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _db.Users.Add(user);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }


        // =========================
        // Edit GET
        // =========================
        public IActionResult Edit(int id)
        {
            var user = _db.Users.Find(id);

            if (user == null)
                return NotFound();

            return View(user);
        }


        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var oldUser = _db.Users.Find(user.Id);

                if (oldUser == null)
                    return NotFound();

                oldUser.Name = user.Name;
                oldUser.UserName = user.UserName;
                oldUser.Email = user.Email;
                oldUser.IsLocked = user.IsLocked;


                if (!string.IsNullOrEmpty(user.Password))
                {

                    oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }


        // =========================
        // Delete GET
        // =========================
        public IActionResult Delete(int id)
        {
            var user = _db.Users.Find(id);

            if (user == null)
                return NotFound();

            return View(user);
        }



        // POST
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var user = _db.Users.Find(id);

            if (user == null)
                return NotFound();

            _db.Users.Remove(user);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}