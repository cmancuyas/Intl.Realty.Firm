using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;

namespace Intl.Realty.Firm.Repository.IRepository
{
    public interface IDocumentTypeAssignmentRepository : IRepository<DocumentTypeAssignment>
    {
        Task UpdateAsync(DocumentTypeAssignment model);
    }
}

