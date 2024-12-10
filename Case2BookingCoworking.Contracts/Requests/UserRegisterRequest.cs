using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Contracts.Requests
{
    public record UserRegisterRequest
     (
         string Name,
         [EmailAddress]
        string Email,
         [MinLength(8)]
        string Password
     );
}
