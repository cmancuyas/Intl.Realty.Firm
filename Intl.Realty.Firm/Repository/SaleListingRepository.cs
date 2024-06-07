using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class SaleListingRepository : Repository<SaleListing>, ISaleListingRepository
    {
        private ApplicationDbContext _db;
        public SaleListingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(SaleListing model)
        {
            _db.SaleListings.Update(model);
            return _db.SaveChangesAsync();
        }
    }
}
