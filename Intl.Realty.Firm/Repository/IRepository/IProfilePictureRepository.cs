using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IProfilePictureRepository : IRepository<ProfilePicture>
    {
        Task UpdateAsync(ProfilePicture model);
    }
}
