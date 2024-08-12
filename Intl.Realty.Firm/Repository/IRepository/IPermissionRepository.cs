using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task UpdateAsync(Permission model);
    }
}
