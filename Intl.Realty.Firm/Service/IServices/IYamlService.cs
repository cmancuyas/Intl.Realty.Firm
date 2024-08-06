namespace Intl.Realty.Firm.Service.IServices
{
    public interface IYamlService
    {
        byte[] Write<T>(IList<T> registers);
    }
}
