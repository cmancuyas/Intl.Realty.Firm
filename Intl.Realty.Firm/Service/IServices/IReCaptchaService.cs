namespace Intl.Realty.Firm.Service.IServices
{
    public interface IReCaptchaService
    {
        Task<bool> VerifyReCaptcha(string token);
        Task<bool> VerifyLocalReCaptcha(string token);
    }
}
