using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCategories()
        {
            IList<Category> categories = new List<Category>
            {
                new Category { Id = 1, CategoryName = "Electronics", Description = "Electronic devices and accessories" },
                new Category { Id = 2, CategoryName = "Home Appliances", Description = "Appliances used at home" },
                new Category { Id = 3, CategoryName = "Office Supplies", Description = "Items used for office work" }
            };

            return Ok(categories);
        }

        public IActionResult GetCategoriesFront()
        {
            IList<Category> categories = new List<Category>
            {
                new Category { Id = 1, CategoryName = "Electronics", Description = "Electronic devices and accessories" },
                new Category { Id = 2, CategoryName = "Home Appliances", Description = "Appliances used at home" },
                new Category { Id = 3, CategoryName = "Office Supplies", Description = "Items used for office work" }
            };

            return View("Index", categories);
        }
    }
}