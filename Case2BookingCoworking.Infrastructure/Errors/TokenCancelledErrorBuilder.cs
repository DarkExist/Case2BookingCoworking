using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Infrastructure.Errors
{
    public sealed class TokenCancelledErrorBuilder : BaseErrorBuilder
    {
        public TokenCancelledErrorBuilder()
        {
            _errorType = 499;
            _errorCode = "Client Closed Request";
            _errorDescription = "A Timeout Occured";
        }
    }
}
