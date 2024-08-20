using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class DealStatusRepository : Repository<DealStatus>, IDealStatusRepository
    {
        private ApplicationDbContext _db;
        public DealStatusRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(DealStatus model)
        {
            _db.DealStatuses.Update(model);
            return _db.SaveChangesAsync();
        }
    }

}
