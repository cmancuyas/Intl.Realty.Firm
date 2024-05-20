using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Repository.IRepository;

namespace Intl.Realty.Firm.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IIRFDealRepository IRFDeal { get; private set; }
        public ITransactionTypeRepository TransactionType { get; private set; }
        public IDocumentTypeRepository DocumentType { get; private set; }
        public IDocumentTypeAssignmentRepository DocumentTypeAssignment { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            IRFDeal = new IRFDealRepository(_db);
            TransactionType = new TransactionTypeRepository(_db);
            DocumentType = new DocumentTypeRepository(_db);
            DocumentTypeAssignment = new DocumentTypeAssignmentRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
