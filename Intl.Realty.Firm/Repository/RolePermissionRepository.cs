using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;
using System.Data.Entity;

namespace Intl.Realty.Firm.Repository
{
    public class RolePermissionRepository : Repository<RolePermission>, IRolePermissionRepository
    {
        private ApplicationDbContext _db;
        public RolePermissionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(RolePermission model)
        {
            _db.RolePermissions.Update(model);
            return _db.SaveChangesAsync();
        }
    }

}
