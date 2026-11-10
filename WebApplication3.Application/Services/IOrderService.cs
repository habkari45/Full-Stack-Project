using WebApplication3.Domain.Models;

namespace WebApplication3.Application.Services
{
    public interface IOrderService
    {
        Task<int> MakeOrder(int userId);

        Task<List<Order>> GetMyOrders(int userId);

        Task<IEnumerable<Order>> GetAllOrders();

        Task<bool> UpdateStatus(int orderId, string newStatus);
    }
}