using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.Auxiliary;
using Intl.Realty.Firm.Repository.IRepository;
using System.Linq.Expressions;

namespace Intl.Realty.Firm.Repository
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        private ApplicationDbContext _db;
        public AccountRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task<User> GetCurrentUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetCurrentUserByUserName(string UserName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Role>> GetRoles()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetUserPermissions(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
