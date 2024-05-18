using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class IRFDealRepository : Repository<IRFDeal>, IIRFDealRepository
    {
        private ApplicationDbContext _db;
        public IRFDealRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(IRFDeal obj)
        {
            _db.IRFDeals.Update(obj);
        }
    }
}
