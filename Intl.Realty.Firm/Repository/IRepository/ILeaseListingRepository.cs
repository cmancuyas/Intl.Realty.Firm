using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface ILeaseListingRepository : IRepository<LeaseListing>
    {
        Task UpdateAsync(LeaseListing model);
    }
}
