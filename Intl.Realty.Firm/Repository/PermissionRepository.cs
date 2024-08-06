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

        public async Task<List<Permission>> GetPermissionsByRoleId(int roleId)
        {
            var permissions = await _db.RolePermissions
                                    .Where(x => x.RoleId == roleId)
                                    .Select(x => x.Permission)
                                    .ToListAsync();
            return permissions!;
        }

        public Task UpdateAsync(Permission model)
        {
            _db.Permissions.Update(model);
            return _db.SaveChangesAsync();
        }
    }

}
