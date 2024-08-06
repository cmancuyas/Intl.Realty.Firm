namespace Intl.Realty.Firm.Service.IServices
{
    public interface IJsonService
    {
        byte[] Write<T>(IList<T> registers);
    }
}
