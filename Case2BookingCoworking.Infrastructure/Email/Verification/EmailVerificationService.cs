using Case2BookingCoworking.Application.Abstract.Email;
using Case2BookingCoworking.Infrastructure.Email.Verification.Depends;
using ErrorOr;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Infrastructure.Email.Verification
{
    public class EmailVerificationService : BaseEmailSender, IEmailVerificationService
    {
        private IVerificationCodeGenerator _verificationCodeGenerator;
        private IConcurrentVerificationCodeStorage _verificationCodeStorage;
        private ICodeVerificationService _codeVerificationService;

        public EmailVerificationService(
            IConfiguration configuration,
            IVerificationCodeGenerator verificationCodeGenerator,
            IConcurrentVerificationCodeStorage verificationCodeStorage,
            ICodeVerificationService codeVerificationService)
        {
            var emailOptions = configuration.GetRequiredSection("EmailOptions");

            Host = new HostDetails
            (
                hostName: emailOptions.GetRequiredSection("SmtpMailHost").Value,
                hostPort: int.Parse(emailOptions.GetRequiredSection("SmtpMailSecurePort").Value)
            );

            EmailAgent = new EmailAgentCredentials
            (
                name: emailOptions.GetRequiredSection("EmailAgentName").Value,
                password: emailOptions.GetRequiredSection("EmailAgentPassword").Value
            );

            _verificationCodeGenerator = verificationCodeGenerator;
            _verificationCodeStorage = verificationCodeStorage;
            _codeVerificationService = codeVerificationService;
        }

        public ErrorOr<VerificationResult> VerifyCode(string email, string code)
        {
            return _codeVerificationService.Verify(email, code);
        }

        public async Task<ErrorOr<Success>> SendRegistrationCodeMessageAsync(string destEmail, CancellationToken cancellationToken)
        {
            var verificationCode = new VerificationCodeDetails(_verificationCodeGenerator.Generate(), DateTime.UtcNow);

            var registrationCodeMessange = new VerificationMessage(EmailAgent.Name, destEmail, verificationCode.Code);

            var result = await base.SendEmailMessageAsync(registrationCodeMessange, cancellationToken);

            if (result.IsError)
            {
                return result.Errors;
            }

            try
            {
                _verificationCodeStorage.TryAddCode(email: destEmail, code: verificationCode);

            }
            catch (Exception ex)
            {
                return Error.Failure(description: "Failure on adding verification code to the server");
            }
            finally
            {
                ClearFromOutdatedCodes();
            }

            return Result.Success;
        }

        private void ClearFromOutdatedCodes()
        {
            int maxVerificationCodesCount = 100;

            if (_verificationCodeStorage.GetCodesCount() >= maxVerificationCodesCount)
            {
                _verificationCodeStorage.TryClearFromOutdatedCodes(_codeVerificationService.GetCodeLifeTime());
            }
        }
    }
}
