using ErrorOr;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Infrastructure.Email.Verification.Depends
{
    public class ConcurrentVerificationCodeStorage : IConcurrentVerificationCodeStorage
    {
        private static ConcurrentDictionary<string, VerificationCodeDetails> _verificationCodesDetails = new();

        public int GetCodesCount()
        {
            return _verificationCodesDetails.Count;
        }

        public ErrorOr<Success> TryAddCode(string email, VerificationCodeDetails code)
        {
            try
            {
                _verificationCodesDetails.TryAdd(key: email, value: code);
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }

            return Result.Success;
        }

        public ErrorOr<int> TryClearFromOutdatedCodes(TimeSpan codeLifetime)
        {
            int deletedCodes = 0;

            foreach (var codeDetails in _verificationCodesDetails)
            {
                if (codeDetails.Value.SendingTime.Add(codeLifetime) >= DateTime.UtcNow)
                {
                    TryDeleteCode(codeDetails.Key);
                    deletedCodes++;
                }
            }

            return deletedCodes;
        }

        public ErrorOr<Success> TryDeleteCode(string email)
        {
            if (_verificationCodesDetails.TryRemove(email, out _))
            {
                return Result.Success;
            }

            return Error.Failure(description: $"Deletion code from: {email} was failed");
        }
        public ErrorOr<VerificationCodeDetails> TryGetCode(string key)
        {
            try
            {
                var result = _verificationCodesDetails.TryGetValue(key, out var codeDetails);

                if (result == true)
                {
                    return codeDetails;
                }
                else
                {
                    return Error.NotFound(description: $"Record with key: {key} was not found");
                }
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }
        }
    }
}
