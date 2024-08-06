namespace Intl.Realty.Firm.Service.IServices
{
    public interface IHtmlService
    {
        byte[] Write<T>(IList<T> registers);
    }
}
