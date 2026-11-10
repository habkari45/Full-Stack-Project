using WebApplication3.Infrastructure.Data;
using WebApplication3.Infrastructure.Repositories.Base;

namespace WebApplication3.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            // SECURITY OFFICER
            SecurityOfficerRepository =
                new SecurityOfficerRepository(_db);

            // SECURITY INCIDENT
            SecurityIncidentRepository =
                new SecurityIncidentRepository(_db);

            // USER
            UserRepository =
                new UserRepository(_db);

            // PRODUCT
            ProductRepository =
                new ProductRepository(_db);

            // CATEGORY
            CategoryRepository =
                new CategoryRepository(_db);

            // CART
            CartRepository =
                new CartRepository(_db);

            // ORDER
            OrderRepo =
                new OrderRepository(_db);
        }

        // SECURITY OFFICER
        public ISecurityOfficerRepository SecurityOfficerRepository
        {
            get;
        }

        // SECURITY INCIDENT
        public ISecurityIncidentRepository SecurityIncidentRepository
        {
            get;
        }

        // USER
        public IUserRepository UserRepository
        {
            get;
        }

        // PRODUCT
        public IProductRepository ProductRepository
        {
            get;
        }

        // CATEGORY
        public ICategoryRepository CategoryRepository
        {
            get;
        }

        // CART
        public ICartRepository CartRepository
        {
            get;
        }

        // ORDER
        public IOrderRepository OrderRepo
        {
            get;
        }
    }
}