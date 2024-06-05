using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        private ApplicationDbContext _db;
        public ProvinceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(Province model)
        {
            _db.Provinces.Update(model);
            return _db.SaveChangesAsync();
        }
    }
}
