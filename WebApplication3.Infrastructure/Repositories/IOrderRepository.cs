using WebApplication3.Domain.Models;

namespace WebApplication3.Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();

        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

        Task<Order?> GetOrderByIdAsync(int orderId);

        Task AddOrderAsync(Order order);

        Task UpdateOrderAsync(Order order);

        Task DeleteOrderAsync(int orderId);
    }
}