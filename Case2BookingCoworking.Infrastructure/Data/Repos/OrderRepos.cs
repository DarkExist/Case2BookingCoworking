using Case2BookingCoworking.Application.Abstract.Repos;
using Case2BookingCoworking.Core.Domain.Entities;
using Case2BookingCoworking.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Infrastructure.Data.Repos
{
    internal class OrderRepos : BaseRepository<Order>, IOrderRepos
    {
        public OrderRepos(BookingContext context) : base(context)
        {
        }
    }
}
