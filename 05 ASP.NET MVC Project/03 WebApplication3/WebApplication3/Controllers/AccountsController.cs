using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication3.Infrastructure.Data;

namespace WebApplication3.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AppDbContext _db;

        public AccountsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginConfirm(
            string email,
            string password)
        {
            var user = _db.Users.FirstOrDefault(
                x => x.Email == email);

            if (user != null &&
                !user.IsLocked &&
                BCrypt.Net.BCrypt.Verify(
                    password,
                    user.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(
                        ClaimTypes.Name,
                        user.Name),

                    new Claim(
                        ClaimTypes.Email,
                        user.Email)
                };

                var identity =
                    new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                var principal =
                    new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                return RedirectToAction(
                    "Index",
                    "Home");
            }

            ViewBag.Error =
                "Invalid Email or Password";

            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}