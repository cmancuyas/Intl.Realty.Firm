using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class LeaseListingRepository : Repository<LeaseListing>, ILeaseListingRepository
    {
        private ApplicationDbContext _db;
        public LeaseListingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(LeaseListing model)
        {
            _db.LeaseListings.Update(model);
            return _db.SaveChangesAsync();
        }
    }
}
