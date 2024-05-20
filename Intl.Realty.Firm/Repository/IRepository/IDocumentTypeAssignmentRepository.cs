using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IDocumentTypeAssignmentRepository : IRepository<DocumentTypeAssignment>
    {
        void Update(DocumentTypeAssignment obj);
    }
}

