using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task UpdateAsync(Customer model);
    }
}
