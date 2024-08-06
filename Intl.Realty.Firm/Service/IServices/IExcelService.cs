namespace Intl.Realty.Firm.Service.IServices
{
    public interface IExcelService
    {
        Task<byte[]> Write<T>(IList<T> modelList);
    }
}
