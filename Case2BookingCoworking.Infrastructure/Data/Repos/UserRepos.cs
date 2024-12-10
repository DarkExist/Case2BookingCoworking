using Case2BookingCoworking.Application.Abstract.Repos;
using Case2BookingCoworking.Core.Domain.Entities;
using Case2BookingCoworking.Infrastructure.Data.Context;
using Case2BookingCoworking.Infrastructure.Errors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Case2BookingCoworking.Infrastructure.Data.Repos
{
    internal class UserRepos : BaseRepository<User>, IUserRepos
    {
        public UserRepos(BookingContext context) : base(context) { }
        public async Task<ErrorOr<User>> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Email == email).Include(u => u.Roles).FirstOrDefaultAsync(cancellationToken);

                if (user is null)
                {
                    return Error.NotFound(description: $"User with email: {email} not found");
                }

                return user;
            }
            catch (OperationCanceledException)
            {

                return new TokenCancelledErrorBuilder().BuildError();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + $" can't found User with email: {email}");
            }
        }
    }
}
