using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Services;

namespace WebApplication3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> MakeOrder(int userId)
        {
            if (userId <= 0)
                return BadRequest("Invalid user ID.");

            try
            {
                int orderId = await _orderService.MakeOrder(userId);

                if (orderId == 0)
                    return BadRequest("Cart is empty");

                return Ok(new
                {
                    message = "Order created successfully",
                    orderId
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetMyOrders(int userId)
        {
            var orders = await _orderService.GetMyOrders(userId);

            var result = orders.Select(order => new
            {
                order.Id,
                order.UserId,
                order.CreatedAt,
                order.Status,
                order.TotalAmount,

                Items = order.Items.Select(item => new
                {
                    item.Id,
                    item.ProductId,
                    ProductName = item.Product?.Name,
                    item.Price,
                    item.Quantity
                }).ToList()
            }).ToList();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();

            return Ok(orders.Select(order => new
            {
                order.Id,
                order.UserId,
                order.CreatedAt,
                order.Status,
                order.TotalAmount,

                Items = order.Items.Select(item => new
                {
                    item.Id,
                    item.ProductId,
                    ProductName = item.Product?.Name,
                    item.Price,
                    item.Quantity
                }).ToList()
            }));
        }

        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateStatus(
            int orderId,
            [FromBody] UpdateOrderStatusRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Status))
            {
                return BadRequest("Status is required.");
            }

            bool updated = await _orderService.UpdateStatus(
                orderId,
                request.Status);

            if (!updated)
            {
                return BadRequest(

                    "Order not found or invalid status.");
            }

            return Ok(new
            {
                message = "Order status updated successfully"
            });
        }

        public class UpdateOrderStatusRequest
        {
            public string Status { get; set; } = string.Empty;
        }
    }
}