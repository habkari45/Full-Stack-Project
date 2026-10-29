using WebApplication3.Domain.Models;

namespace WebApplication3.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);

        User? GetByEmail(string email);

        void Add(User user);
    }
}
