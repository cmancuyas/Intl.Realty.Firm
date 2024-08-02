using Intl.Realty.Firm.Models.Models.Auxiliary;
using System.Diagnostics;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IActivityLogRepository : IRepository<ActivityLog>
    {
        Task<List<ActivityLog>> GetRecordsByUserIdAsync(int userId);
    }
}
