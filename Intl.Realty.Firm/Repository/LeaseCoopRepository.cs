using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class LeaseCoopRepository : Repository<LeaseCoop>, ILeaseCoopRepository
    {
        private ApplicationDbContext _db;
        public LeaseCoopRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(LeaseCoop model)
        {
            _db.LeaseCoops.Update(model);
            return _db.SaveChangesAsync();
        }
    }
}
