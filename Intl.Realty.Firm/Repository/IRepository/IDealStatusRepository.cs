using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IDealStatusRepository : IRepository<DealStatus>
    {
        Task UpdateAsync(DealStatus model);
    }
}
