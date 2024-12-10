using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Application.Abstract.Email
{
    public interface IConcurrentVerificationCodeStorage
    {
        public ErrorOr<VerificationCodeDetails> TryGetCode(string key);
        public ErrorOr<Success> TryAddCode(string email, VerificationCodeDetails code);
        public ErrorOr<Success> TryDeleteCode(string email);
        public ErrorOr<int> TryClearFromOutdatedCodes(TimeSpan codeLifetime);

        public int GetCodesCount();
    }
}
