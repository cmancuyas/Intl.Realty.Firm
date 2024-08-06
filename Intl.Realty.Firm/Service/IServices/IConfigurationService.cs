using Intl.Realty.Firm.Service.IServices.IConfiguration;

namespace Intl.Realty.Firm.Service.IServices
{
    public interface IConfigurationService : IConfigurationReCaptcha
    {
        string GetDefaultUploadPathFromConfig();

    }
}
