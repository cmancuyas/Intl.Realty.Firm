using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface ISaleListingRepository : IRepository<SaleListing>
    {
        void Update(SaleListing obj);
    }
}
