using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Intl.Realty.Firm.Repository
{
    public class RolePermissionRepository : Repository<RolePermission>, IRolePermissionRepository
    {
        private ApplicationDbContext _db;
        public RolePermissionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<List<Permission>> GetRolePermissionsByRoleId(int roleId)
        {
            var rolePermissions = await _db.RolePermissions
                                    .Include(x => x.Permission)
                                    .Where(x => x.RoleId == roleId)
                                    .Select(x => x.Permission)
                                    .ToListAsync();
            return rolePermissions!;
        }
        public Task UpdateAsync(RolePermission model)
        {
            _db.RolePermissions.Update(model);
            return _db.SaveChangesAsync();
        }
    }

}
