using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task UpdateAsync(User model);
    }
}
