using Case2BookingCoworking.Core.Domain.Entities;
using ErrorOr;

namespace Case2BookingCoworking.Application.Abstract.Repos
{
    public interface IUserRepos : IRepos<User>
    {
        public Task<ErrorOr<User>> GetUserByEmail(string email, CancellationToken cancellationToken);
    }
}
