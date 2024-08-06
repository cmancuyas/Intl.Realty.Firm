namespace Intl.Realty.Firm.Service.IServices.IConfiguration
{
    public interface IConfigurationReCaptcha
    {
        string GetSiteKey();

        // For Local Host
        string GetLocalSiteKey();
    }
}
