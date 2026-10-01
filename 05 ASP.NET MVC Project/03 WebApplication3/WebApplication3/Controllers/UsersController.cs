using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<User> users = _db.Users.ToList();

            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _db.Users.Add(user);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            User? user = _db.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            User? userFromDb = _db.Users.Find(user.Id);

            if (userFromDb == null)
            {
                return NotFound();
            }

            userFromDb.Name = user.Name;
            userFromDb.UserName = user.UserName;
            userFromDb.Email = user.Email;
            userFromDb.IsLocked = user.IsLocked;

            if (!string.IsNullOrEmpty(user.Password))
            {
                userFromDb.Password =
                    BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            User? user = _db.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(User user)
        {
            User? userFromDb = _db.Users.Find(user.Id);

            if (userFromDb == null)
            {
                return NotFound();
            }

            _db.Users.Remove(userFromDb);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}