using Microsoft.EntityFrameworkCore;
using WebApplication3.Domain.Models;
using WebApplication3.Infrastructure.Data;

namespace WebApplication3.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;

        public OrderRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _db.Orders
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(
            int userId)
        {
            return await _db.Orders
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _db.Orders
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _db.Orders.AddAsync(order);
        }

        public Task UpdateOrderAsync(Order order)
        {
            _db.Orders.Update(order);

            return Task.CompletedTask;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _db.Orders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
                return;

            _db.OrderItems.RemoveRange(order.Items);

            var notifications = await _db.Notifications
                .Where(x => x.OrderId == orderId)
                .ToListAsync();

            _db.Notifications.RemoveRange(notifications);

            _db.Orders.Remove(order);
        }
    }
}