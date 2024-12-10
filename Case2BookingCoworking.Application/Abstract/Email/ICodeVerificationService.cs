using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Application.Abstract.Email
{
    public interface ICodeVerificationService
    {
        ErrorOr<VerificationResult> Verify(string email, string code);

        TimeSpan GetCodeLifeTime();
    }
}
