using Intl.Realty.Firm.Models.Models;
namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IRolePermissionRepository : IRepository<RolePermission>
    {
        Task UpdateAsync(RolePermission model);
        Task<List<Permission>> GetRolePermissionsByRoleId(int roleId);
    }
}
