using LegnicaIT.BusinessLogic.Configuration;
using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace LegnicaIT.BusinessLogic.Helpers
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient client;
        private readonly IOptions<EmailServiceCredentials> settings;

        public EmailService(IOptions<EmailServiceCredentials> settings)
        {
            this.settings = settings;
            client = new SmtpClient()
            {
                ServerCertificateValidationCallback = (s, c, h, e) => true,
            };

            // Note: since we don't have an OAuth2 token, disable
            // the XOAUTH2 authentication mechanism.
            client.AuthenticationMechanisms.Remove("XOAUTH2");
        }

        //TODO: Add logging on failed Task
        public async Task SendEmailAsync(string emailAddress, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("JwtManager", settings.Value.Login));
            emailMessage.To.Add(new MailboxAddress("", emailAddress));
            emailMessage.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = message
            };

            emailMessage.Body = builder.ToMessageBody();
            
            client.Connect(settings.Value.Host, settings.Value.Port, settings.Value.UseSsl);

            // Note: only needed if the SMTP server requires authentication
            client.Authenticate(settings.Value.Login, settings.Value.Password);
            await client.SendAsync(emailMessage).ConfigureAwait(false);
            await client.DisconnectAsync(true).ConfigureAwait(false);
        }
    }
}
