using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Services;

namespace WebApplication3.MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Dashboard - All Orders
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrders();

            return View(orders);
        }

        // Update Order Status
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(
            int orderId,
            UpdateOrderStatusDto dto)
        {
            bool updated = await _orderService.UpdateStatus(
                orderId,
                dto.Status
            );

            if (!updated)
            {
                TempData["Error"] = "Invalid order or status";

                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Order status updated successfully";

            return RedirectToAction(nameof(Index));
        }
    }

    public class UpdateOrderStatusDto
    {
        public string Status { get; set; } = "";
    }
}