using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Contracts.Requests
{
    public record UserLoginRequest
    (
        [EmailAddress]
        string Email,
        string Password
    );
}
