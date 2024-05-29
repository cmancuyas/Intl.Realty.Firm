using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class UserTypeRepository : Repository<UserType>, IUserTypeRepository
    {
        private ApplicationDbContext _db;
        public UserTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(UserType model)
        {
            _db.UserTypes.Update(model);
            return _db.SaveChangesAsync();
        }
    }
}
