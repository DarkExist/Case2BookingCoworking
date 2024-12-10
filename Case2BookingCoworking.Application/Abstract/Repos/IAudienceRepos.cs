using Case2BookingCoworking.Core.Domain.Entities;
using ErrorOr;

namespace Case2BookingCoworking.Application.Abstract.Repos
{
    public interface IAudienceRepos : IRepos<Audience>
    {
		public Task<ErrorOr<Audience>> GetAudienceByNumberAsync(string number);

	}
}
