using ErrorOr;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Infrastructure.Email.Verification.Depends
{
    public class VerificationMessage : MimeMessage
    {
        public VerificationMessage(string mailAgentName, string destMail, string code)
        {
            From.Add(new MailboxAddress("Weight Control", mailAgentName));
            To.Add(new MailboxAddress("Получатель", destMail));

            Subject = "Код подтверждения";
            Body = new BodyBuilder() { HtmlBody = $"<div style=\"color: green;\">Код подтверждения: {code}</div>" }.ToMessageBody();
        }
    }
}
