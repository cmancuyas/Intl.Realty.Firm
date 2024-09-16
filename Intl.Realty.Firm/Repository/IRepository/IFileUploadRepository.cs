using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IFileUploadRepository : IRepository<FileUpload>
    {
        Task UpdateAsync(FileUpload model);
    }
}
