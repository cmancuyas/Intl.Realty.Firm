using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface ILeaseCoopRepository : IRepository<LeaseCoop>
    {
        Task UpdateAsync(LeaseCoop model);
    }
}
