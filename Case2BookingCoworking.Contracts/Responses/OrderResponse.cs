using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Contracts.Responses
{
    public record OrderResponse
    (
        string audienceNumber,
        DateTime startOfBooking,
        DateTime endOfBooking,
        UserResponse user
        );
}
