using WebApplication3.Infrastructure.Repositories;

namespace WebApplication3.Infrastructure.Repositories.Base
{
    public interface IUnitOfWork
    {
        ISecurityIncidentRepository SecurityIncidentRepository { get; }

        ISecurityOfficerRepository SecurityOfficerRepository { get; }
    }
}
