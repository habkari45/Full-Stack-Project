using Microsoft.EntityFrameworkCore;
using WebApplication3.Domain.Models;
using WebApplication3.Infrastructure.Data;
using WebApplication3.Infrastructure.Repositories;
using WebApplication3.Infrastructure.Repositories.Base;

namespace WebApplication3.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(
            AppDbContext db,
            IUnitOfWork unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _unitOfWork.OrderRepo.GetAll();
        }

        public async Task<List<Order>> GetMyOrders(int userId)
        {
            var orders = await _unitOfWork.OrderRepo
                .GetOrdersByUserIdAsync(userId);

            return orders.ToList();
        }

        public async Task<int> MakeOrder(int userId)
        {
            await using var transaction =
                await _db.Database.BeginTransactionAsync();

            var cartItems = await _db.CartItems
                .Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return 0;
            }

            if (cartItems.Any(x =>
                x.Product == null || x.Quantity <= 0))
            {
                throw new InvalidOperationException(
                    "Cart contains invalid items.");
            }

            var order = new Order
            {
                UserId = userId,

                Status = "Pending",

                Items = cartItems.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,

                    Quantity = x.Quantity,

                    Price = x.Product!.Price

                }).ToList()
            };

            order.TotalAmount = order.Items.Sum(
                x => x.Price * x.Quantity
            );

            await _unitOfWork.OrderRepo.AddOrderAsync(order);

            await _db.SaveChangesAsync();

            _db.Notifications.Add(new Notification
            {
                UserId = userId,

                OrderId = order.Id,

                Title = "Order Created",

                Message =
                    $"Your order with ID {order.Id} has been placed successfully."
            });

            _db.CartItems.RemoveRange(cartItems);

            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            return order.Id;
        }

        public async Task<bool> UpdateStatus(
            int orderId,
            string newStatus)
        {
            var order = await _unitOfWork.OrderRepo
                .GetOrderByIdAsync(orderId);

            if (order == null)
            {
                return false;
            }

            var allowedStatuses = new[]
            {
                "Pending",
                "Processing",
                "Shipped",
                "Delivered",
                "Cancelled"
            };

            if (!allowedStatuses.Contains(newStatus))
            {
                return false;
            }

            if (order.Status == newStatus)
            {
                return true;
            }

            await using var transaction =
                await _db.Database.BeginTransactionAsync();

            order.Status = newStatus;

            await _unitOfWork.OrderRepo.UpdateOrderAsync(order);

            _db.Notifications.Add(new Notification
            {
                UserId = order.UserId,

                OrderId = order.Id,

                Title = "Order Status Updated",

                Message =
                    $"Your order with ID {order.Id} status has been updated to '{newStatus}'."
            });

            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            return true;
        }
    }
}