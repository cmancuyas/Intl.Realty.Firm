using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IUserTypeRepository : IRepository<UserType>
    {
        Task UpdateAsync(UserType model);
    }
}
