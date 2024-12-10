using Case2BookingCoworking.Application.Abstract.Repos;
using Case2BookingCoworking.Core.Domain.Entities;
using Case2BookingCoworking.Infrastructure.Data.Context;
using Case2BookingCoworking.Infrastructure.Errors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Case2BookingCoworking.Infrastructure.Data.Repos
{
    public class AudienceRepos : BaseRepository<Audience>, IAudienceRepos
    {
        public AudienceRepos(BookingContext context) : base(context) { }

		public async Task<ErrorOr<Audience>> GetAudienceByNumberAsync(string number)
		{
			try
			{
				return await _context.Audiences.FirstAsync(a => a.Number == number);
			}
			catch (OperationCanceledException)
			{
				return new TokenCancelledErrorBuilder().BuildError();
			}
			catch (Exception ex)
			{
				return Error.Failure("DB is not available");
			}
		}
	}
}
