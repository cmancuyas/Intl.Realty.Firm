using Intl.Realty.Firm.Models.Models;
using System.Diagnostics;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IModuleRepository : IRepository<Module>
    {
        Task UpdateAsync(Module model);
    }
}
