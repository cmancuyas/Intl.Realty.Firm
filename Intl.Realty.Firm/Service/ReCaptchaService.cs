using Intl.Realty.Firm.Service.IServices;
using Newtonsoft.Json;

namespace Intl.Realty.Firm.Service
{
    public class ReCaptchaService : IReCaptchaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationService _configurationService;

        public ReCaptchaService(IConfigurationService configurationService)
        {

            _httpClient = new HttpClient();
            _configurationService = configurationService;
        }

        public async Task<bool> VerifyLocalReCaptcha(string token)
        {
            var secretKey = _configurationService.GetLocalSiteKey();
            var response = await _httpClient.GetStringAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}");
            dynamic result = JsonConvert.DeserializeObject(response);
            return result.success;
        }

        public async Task<bool> VerifyReCaptcha(string token)
        {
            var secretKey = _configurationService.GetSiteKey();
            var response = await _httpClient.GetStringAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}");
            dynamic result = JsonConvert.DeserializeObject(response);
            return result.success;
        }
    }
}
