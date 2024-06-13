using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class ProfilePictureRepository : Repository<ProfilePicture>, IProfilePictureRepository
    {
        private ApplicationDbContext _db;
        public ProfilePictureRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(ProfilePicture model)
        {
            _db.ProfilePictures.Update(model);
            return _db.SaveChangesAsync();
        }
    }

}
