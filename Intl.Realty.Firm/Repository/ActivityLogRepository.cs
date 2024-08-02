using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models.Auxiliary;
using Intl.Realty.Firm.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Intl.Realty.Firm.Repository
{
    public class ActivityLogRepository : Repository<ActivityLog>, IActivityLogRepository
    {
        private ApplicationDbContext _db;
        public ActivityLogRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<ActivityLog>> GetRecordsByUserIdAsync(int userId)
        {
            return await _db.Logs
                .Where(x => x.CreatedBy == userId)
                .Include(x => x.Module)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task UpdateAsync(ActivityLog model)
        {
            _db.Logs.Update(model);
            return _db.SaveChangesAsync();
        }
    }
}
