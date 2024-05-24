using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DocumentTypeAssignmentMapper
    {
        public static DocumentTypeAssignmentViewModel ToDocumentTypeAssignmentViewModel(this DocumentTypeAssignment model)
        {
            return new DocumentTypeAssignmentViewModel
            {
                Id = model.Id,
                DocumentTypeId = model.DocumentTypeId,
                TransactionTypeId = model.TransactionTypeId
            };
        }
        public static CreateDocumentTypeAssignmentViewModel ToCreateDocumentTypeAssignmentViewModel(this DocumentTypeAssignment model)
        {
            return new CreateDocumentTypeAssignmentViewModel
            {
                Id = model.Id,
                DocumentTypeId = model.DocumentTypeId,
                TransactionTypeId = model.TransactionTypeId
            };
        }
    }

}
