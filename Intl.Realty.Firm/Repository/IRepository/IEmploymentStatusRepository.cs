using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IEmploymentStatusRepository : IRepository<EmploymentStatus>
    {
        Task UpdateAsync(EmploymentStatus model);
    }
}
