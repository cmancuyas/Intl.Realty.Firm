
using Intl.Realty.Firm.Models.Models.Auxiliary;

namespace Intl.Realty.Firm.Service.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
