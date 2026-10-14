using WebApplication3.Infrastructure.Data;
using WebApplication3.Infrastructure.Repositories;

namespace WebApplication3.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            SecurityIncidentRepository = new SecurityIncidentRepository(_db);
            SecurityOfficerRepository = new SecurityOfficerRepository(_db);
        }

        public ISecurityIncidentRepository SecurityIncidentRepository { get; }

        public ISecurityOfficerRepository SecurityOfficerRepository { get; }
    }
}
