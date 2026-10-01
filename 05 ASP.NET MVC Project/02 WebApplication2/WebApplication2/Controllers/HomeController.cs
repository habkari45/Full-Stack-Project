using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;
using Microsoft.AspNetCore.Authorization;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(
            ILogger<HomeController> logger,
            AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.ProductsCount = _db.Products.Count();
            ViewBag.CategoriesCount = _db.Categories.Count();
            ViewBag.CustomersCount = _db.Customers.Count();
            ViewBag.UsersCount = _db.Users.Count();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                ?? HttpContext.TraceIdentifier
            });
        }
    }
}