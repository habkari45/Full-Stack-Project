using WebApplication2.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AppDbContext _db;

        public AccountsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginConfirm(string email, string password)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            bool isPasswordCorrect =
                BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!isPasswordCorrect)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View("Login");
            }

            if (user.IsLocked)
            {
                ModelState.AddModelError("", "Your account is locked");
                return View("Login");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.NameIdentifier, "1")
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction("Login");
        }
    }
}
