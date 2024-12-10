using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Infrastructure.Email.Verification.Depends
{
    public class CodeVerificationService : ICodeVerificationService
    {
        private static int VerificationCodeLifetimeInMinutes = 2;

        private IConcurrentVerificationCodeStorage _codeStorage;

        private readonly TimeSpan _codeExpirationTime = TimeSpan.FromMinutes(VerificationCodeLifetimeInMinutes);

        public CodeVerificationService(IConcurrentVerificationCodeStorage codeStorage)
        {
            _codeStorage = codeStorage;
        }

        public ErrorOr<VerificationResult> Verify(string email, string userCode)
        {
            var result = _codeStorage.TryGetCode(key: email);

            if (result.IsError)
            {
                return result.Errors;
            }

            var serverCode = result.Value;

            if (userCode == serverCode.Code)
            {
                if (serverCode.SendingTime.Add(_codeExpirationTime) >= DateTime.UtcNow)
                {
                    return VerificationResult.Verifed;
                }
                else
                {
                    return VerificationResult.Outdated;
                }
            }

            return VerificationResult.Wrong;
        }

        public TimeSpan GetCodeLifeTime()
        {
            return _codeExpirationTime;
        }
    }
}
