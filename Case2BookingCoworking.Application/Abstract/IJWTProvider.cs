using Case2BookingCoworking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Application.Abstract
{
    public interface IJWTProvider
    {
        string GenerateToken(User user);
    }
}
