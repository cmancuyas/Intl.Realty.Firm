namespace Intl.Realty.Firm.Service.IServices
{
    public interface IXmlService
    {
        byte[] Write<T>(IList<T> registers);
    }
}
