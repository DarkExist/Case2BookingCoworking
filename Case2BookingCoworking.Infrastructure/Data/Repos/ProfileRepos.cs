﻿using Case2BookingCoworking.Application.Abstract.Repos;
using Case2BookingCoworking.Core.Domain.Entities;
using Case2BookingCoworking.Infrastructure.Data.Context;

namespace Case2BookingCoworking.Infrastructure.Data.Repos
{
    internal class ProfileRepos : BaseRepository<Profile>, IProfileRepos
    {
        public ProfileRepos(BookingContext context) : base(context)
        {
        }
    }
}
