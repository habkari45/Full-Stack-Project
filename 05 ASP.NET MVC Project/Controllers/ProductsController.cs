using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProducts()
        {
            IList<Product> products = new List<Product>
    {
        new Product { Id = 1, ProductName = "Smart Watch", Price = 850, Quantity = 7 },
        new Product { Id = 2, ProductName = "Wireless Headphones", Price = 420, Quantity = 15 },
        new Product { Id = 3, ProductName = "Portable Charger", Price = 160, Quantity = 25 }
    };

            return Ok(products);
        }

        public IActionResult GetProductsFront()
        {
            IList<Product> products = new List<Product>
    {
        new Product { Id = 1, ProductName = "Smart Watch", Price = 850, Quantity = 7 },
        new Product { Id = 2, ProductName = "Wireless Headphones", Price = 420, Quantity = 15 },
        new Product { Id = 3, ProductName = "Portable Charger", Price = 160, Quantity = 25 }
    };

            return View("Index", products);
        }
    }
}
