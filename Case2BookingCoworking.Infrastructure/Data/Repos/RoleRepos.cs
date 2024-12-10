using Case2BookingCoworking.Application.Abstract.Repos;
using Case2BookingCoworking.Core.Domain.Entities;
using Case2BookingCoworking.Infrastructure.Data.Context;


namespace Case2BookingCoworking.Infrastructure.Data.Repos
{
    internal class RoleRepos : BaseRepository<Role>, IRoleRepos
    {
        public RoleRepos(BookingContext context) : base(context)
        {
        }
    }
}
