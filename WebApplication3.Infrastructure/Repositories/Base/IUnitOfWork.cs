using WebApplication3.Infrastructure.Repositories;

namespace WebApplication3.Infrastructure.Repositories.Base
{
    public interface IUnitOfWork
    {
        ISecurityIncidentRepository SecurityIncidentRepository { get; }

        ISecurityOfficerRepository SecurityOfficerRepository { get; }

        IUserRepository UserRepository { get; }

        IProductRepository ProductRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        ICartRepository CartRepository { get; }
        IOrderRepository OrderRepo { get; }
    }
}