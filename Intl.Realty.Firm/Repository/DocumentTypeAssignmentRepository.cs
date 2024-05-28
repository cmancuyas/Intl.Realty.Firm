using Intl.Realty.Firm.DataAccess;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;
using Intl.Realty.Firm.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Repository
{
    public class DocumentTypeAssignmentRepository : Repository<DocumentTypeAssignment>, IDocumentTypeAssignmentRepository
    {
        private ApplicationDbContext _db;
        public DocumentTypeAssignmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task UpdateAsync(DocumentTypeAssignment obj)
        {
            _db.DocumentTypeAssignments.Update(obj);
            return _db.SaveChangesAsync();
        }

    }
}

