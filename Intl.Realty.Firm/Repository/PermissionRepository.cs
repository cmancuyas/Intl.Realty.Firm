using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;
using System.Data.Entity;

namespace Intl.Realty.Firm.Repository
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        private ApplicationDbContext _db;
        public PermissionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(Permission model)
        {
            _db.Permissions.Update(model);
            return _db.SaveChangesAsync();
        }
    }

}
