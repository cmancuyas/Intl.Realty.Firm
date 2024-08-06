using Intl.Realty.Firm.Service.IServices;

namespace Intl.Realty.Firm.Service
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetDefaultUploadPathFromConfig()
        {
            var defaultPath = _configuration["FileUpload:DefaultPath"];
            if (defaultPath != null)
            {
                return defaultPath;
            }
            return "";
        }

        public string GetLocalSiteKey()
        {
            return _configuration["GoogleReCaptchaLocalHost:SiteKey"]!;
        }

        public string GetSiteKey()
        {
            return _configuration["GoogleReCaptcha:SiteKey"]!;
        }
    }
}
