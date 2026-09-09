using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;



namespace WebApplication2.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCustomers()
        {
            IList<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1, FullName = "Sultan Ibrahim", Email = "Sultan@email.com", City = "Riyadh" },
                new Customer { Id = 2, FullName = "Sara Ali", Email = "sara@email.com", City = "Jeddah" },
                new Customer { Id = 3, FullName = "Khalid Mohammed", Email = "khalid@email.com", City = "Dammam" }
            };

            return Ok(customers);
        }

        public IActionResult GetCustomersFront()
        {
            IList<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1, FullName = "Sultan Ibrahim", Email = "sultan@email.com", City = "Riyadh" },
                new Customer { Id = 2, FullName = "Sara Ali", Email = "sara@email.com", City = "Jeddah" },
                new Customer { Id = 3, FullName = "Khalid Mohammed", Email = "khalid@email.com", City = "Dammam" }
            };

            return View("Index", customers);
        }
    }
}

