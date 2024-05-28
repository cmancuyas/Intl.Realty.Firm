using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface ISaleListingRepository : IRepository<SaleListing>
    {
        Task UpdateAsync(SaleListing obj);
    }
}
