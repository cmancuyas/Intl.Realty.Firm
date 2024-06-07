using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface ISaleCoopRepository : IRepository<SaleCoop>
    {
        Task UpdateAsync(SaleCoop model);
    }
}
