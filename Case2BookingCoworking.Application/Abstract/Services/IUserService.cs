using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case2BookingCoworking.Contracts.Requests;

namespace Case2BookingCoworking.Application.Abstract.Services
{
    public interface IUserService
    {
        Task<ErrorOr<string>> Login(string Email, string Password);
        Task<ErrorOr<Success>> RegisterIfVerifiedAsync(UserRegisterRequest userRegistrationRequest, string code, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> SendEmailVerificationCode(string email, CancellationToken cancellationToken);
    }
}
