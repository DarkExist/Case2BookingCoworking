using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Application.Abstract.Email
{
    public interface IEmailVerificationService
    {
        Task<ErrorOr<Success>> SendRegistrationCodeMessageAsync(string destEmail, CancellationToken cancellationToken);

        ErrorOr<VerificationResult> VerifyCode(string email, string code);
    }
}
