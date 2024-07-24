namespace Intl.Realty.Firm.Service.IServices
{
    public interface IFileHandlerService
    {
        Task<(string, string, string)> UploadFile(IFormFile file, string directory);
        Task<(byte[], string, string)> DownloadFile(string directory, string fileName, string fileExtension);
        bool DeleteFile(string fullPath);
    }
}
