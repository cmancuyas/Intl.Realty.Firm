using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;
using System.Xml.Linq;

namespace Intl.Realty.Firm.Repository
{
    public class DocumentTypeRepository : Repository<DocumentType>, IDocumentTypeRepository
    {
        private ApplicationDbContext _db;
        public DocumentTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DocumentType obj)
        {
            _db.DocumentTypes.Update(obj);
        }
    }
}

