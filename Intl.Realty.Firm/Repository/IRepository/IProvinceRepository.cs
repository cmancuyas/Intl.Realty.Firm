using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IProvinceRepository : IRepository<Province>
    {
        Task UpdateAsync(Province model);
    }
}
