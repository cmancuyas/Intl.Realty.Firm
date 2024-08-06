using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Service.IServices;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IRolePermissionRepository : IRepository<RolePermission>
    {
        Task UpdateAsync(RolePermission model);
    }
}
