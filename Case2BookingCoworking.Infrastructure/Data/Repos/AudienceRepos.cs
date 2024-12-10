using Case2BookingCoworking.Application.Abstract.Repos;
using Case2BookingCoworking.Core.Domain.Entities;
using Case2BookingCoworking.Infrastructure.Data.Context;
using ErrorOr;

namespace Case2BookingCoworking.Infrastructure.Data.Repos
{
    public class AudienceRepos : BaseRepository<Audience>, IAudienceRepos
    {
        public AudienceRepos(BookingContext context) : base(context) { }
    }
}
