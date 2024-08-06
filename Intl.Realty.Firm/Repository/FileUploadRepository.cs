using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Intl.Realty.Firm.Repository
{
    public class FileUploadRepository : Repository<FileUpload>, IFileUploadRepository
    {
        private ApplicationDbContext _db;
        public FileUploadRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public Task<List<FileUpload>> GetFileUploadsBySaleListingIdAsync(int saleListingId)
        {
            var records =_db.FileUploads
                            .Include(x=>x.TransactionType)
                            .Include(x=>x.SaleListing)
                            .Include(x=>x.DocumentType)
                            .Where(x => x.SaleListingId == saleListingId)
                            .ToListAsync();

            return records;
        }

        public Task UpdateAsync(FileUpload model)
        {
            _db.FileUploads.Update(model);
            return _db.SaveChangesAsync();
        }
    }
}