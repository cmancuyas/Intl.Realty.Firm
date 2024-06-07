using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class SaleCoopRepository : Repository<SaleCoop>, ISaleCoopRepository
    {
        private ApplicationDbContext _db;
        public SaleCoopRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(SaleCoop model)
        {
            _db.SaleCoops.Update(model);
            return _db.SaveChangesAsync();
        }
    }
}
