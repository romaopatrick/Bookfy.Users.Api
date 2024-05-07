using System.Linq.Expressions;
using Bookfy.Users.Api.Domain;

namespace Bookfy.Users.Api.Ports
{
    public interface IUserRepository
    {
        Task<long> Count(Expression<Func<User, bool>> filter, CancellationToken ct);
        Task<User> Create(User user, CancellationToken ct);
        Task<User> Update(User user, CancellationToken ct);
        Task<User> First(Expression<Func<User,bool>> filter, CancellationToken ct);
        Task Inactivate(Guid id, CancellationToken ct);
    }
}