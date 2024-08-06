using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class EmploymentStatusRepository : Repository<EmploymentStatus>, IEmploymentStatusRepository
    {
        private ApplicationDbContext _db;
        public EmploymentStatusRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(EmploymentStatus model)
        {
            _db.EmploymentStatuses.Update(model);
            return _db.SaveChangesAsync();
        }
    }

}
