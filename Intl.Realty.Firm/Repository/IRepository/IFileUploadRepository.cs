using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository.IConfiguration;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IFileUploadRepository : IRepository<FileUpload>, IConfigurationFileUploadRepository
    {
        Task<(string, string)> UploadFile(IFormFile file, string directory);
        Task<(byte[], string, string)> DownloadFile(string directory, string fileName, string fileExtension);
        bool DeleteFile(string fullPath);
    }
}
