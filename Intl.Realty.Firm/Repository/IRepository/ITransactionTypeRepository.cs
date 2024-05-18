using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface ITransactionTypeRepository : IRepository<TransactionType>
    {
        void Update(TransactionType obj);
    }
}
