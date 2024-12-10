using ErrorOr;
using MailKit.Net.Smtp;
using MimeKit;

namespace Case2BookingCoworking.Infrastructure.Email
{
    public abstract class BaseEmailSender
    {
        protected HostDetails Host;

        protected EmailAgentCredentials EmailAgent;

        public BaseEmailSender(HostDetails host, EmailAgentCredentials emailAgent)
        {
            Host = host;
            EmailAgent = emailAgent;
        }

        public BaseEmailSender()
        {

        }

        protected virtual async Task<ErrorOr<Success>> SendEmailMessageAsync(MimeMessage message, CancellationToken cancellationToken)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(host: Host.Name, port: Host.Port, useSsl: true, cancellationToken);
                    await client.AuthenticateAsync(userName: EmailAgent.Name, password: EmailAgent.Password, cancellationToken);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(quit: true, cancellationToken);

                    return Result.Success;
                }
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }
        }

        protected virtual ErrorOr<Success> SendEmailMessage(MimeMessage message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(host: Host.Name, port: Host.Port, useSsl: true);
                    client.Authenticate(userName: EmailAgent.Name, password: EmailAgent.Password);
                    client.Send(message);
                    client.Disconnect(quit: true);

                    return Result.Success;
                }
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }
        }
    }
}
