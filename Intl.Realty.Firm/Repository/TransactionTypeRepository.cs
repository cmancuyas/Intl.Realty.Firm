using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Intl.Realty.Firm.Repository
{
    public class TransactionTypeRepository : Repository<TransactionType>, ITransactionTypeRepository
    {
        private ApplicationDbContext _db;
        public TransactionTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task<TransactionType> GetByNameAsync(string name)
        {
            return _db.TransactionTypes.FirstOrDefaultAsync(x => x.Description == name)!;
        }

        public Task UpdateAsync(TransactionType model)
        {
            _db.TransactionTypes.Update(model);
            return _db.SaveChangesAsync();
        }

    }
}
