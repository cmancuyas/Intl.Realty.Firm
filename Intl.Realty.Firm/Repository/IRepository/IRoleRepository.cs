using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task UpdateAsync(Role model);
    }
}
