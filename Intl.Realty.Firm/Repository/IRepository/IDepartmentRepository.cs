using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task UpdateAsync(Department model);
    }
}
