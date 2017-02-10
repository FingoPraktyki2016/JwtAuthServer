using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using LegnicaIT.BusinessLogic.Configuration;
using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using Microsoft.Extensions.Options;

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

        public async Task SendEmailAsync(string emailAddress, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            var ConfirmEmail = "Confirm Email";
            string buttonStyle = "background-color:#a692ff; border-radius:40px; color:#fff;padding:15px 32px; text-align:center; text-decoration:none; display: inline-block; font-size:100%; margin: 30px 0 0 0;";

            emailMessage.From.Add(new MailboxAddress("JwtManager", settings.Value.Email));
            emailMessage.To.Add(new MailboxAddress("", emailAddress));
            emailMessage.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = @"
            <body>
                   <a class=""button"" style =""" + buttonStyle + @""" href=""" + message + @"""> " + ConfirmEmail + @"</a>
            </body>";

            emailMessage.Body = builder.ToMessageBody();

            client.Connect("smtp.gmail.com", 587, false);
            // Note: only needed if the SMTP server requires authentication
            client.Authenticate(settings.Value.Email, settings.Value.Password);
            await client.SendAsync(emailMessage).ConfigureAwait(false);
            await client.DisconnectAsync(true).ConfigureAwait(false);
        }
    }
}
