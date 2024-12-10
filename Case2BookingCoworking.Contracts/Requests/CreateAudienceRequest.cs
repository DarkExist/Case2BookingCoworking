using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Contracts.Requests
{
    public record CreateAudienceRequest
    (
        string number,
        int capacity,
        List<string> eventTypes,
        string status
        );
}
