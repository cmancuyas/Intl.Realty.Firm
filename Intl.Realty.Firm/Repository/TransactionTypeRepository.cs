using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class TransactionTypeRepository : Repository<TransactionType>, ITransactionTypeRepository
    {
        private ApplicationDbContext _db;
        public TransactionTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TransactionType obj)
        {
            _db.TransactionTypes.Update(obj);
        }
    }
}
