using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class FileDocumentTypeBridgeRepository : Repository<FileDocumentTypeBridge>, IFileDocumentTypeBridgeRepository
    {
        private ApplicationDbContext _db;
        public FileDocumentTypeBridgeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(FileDocumentTypeBridge model)
        {
            _db.FileDocumentTypeBridges.Update(model);
            return _db.SaveChangesAsync();
        }
    }

}
