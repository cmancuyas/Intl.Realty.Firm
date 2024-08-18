using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Intl.Realty.Firm.Service.IServices;
using Intl.Realty.Firm.Models.Models.Auxiliary;

namespace Intl.Realty.Firm.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _mailSettings;
        public EmailService(IOptions<EmailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {

            var smtp = CreateSmtpCredentials;

            var mailMessage = 
                new MailMessage(
                        from: _mailSettings.Email,
                        to: mailRequest.ToEmail,
                        mailRequest.Subject,
                        mailRequest.Body
                        );

            mailMessage.IsBodyHtml = true;

            await smtp.SendMailAsync(mailMessage);
        }

        private SmtpClient CreateSmtpCredentials => new SmtpClient(_mailSettings.Host, _mailSettings.Port)
        {
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Password)
        };
    }
}
