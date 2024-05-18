using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IIRFDealRepository : IRepository<IRFDeal>
    {
        void Update(IRFDeal obj);
    }
}
