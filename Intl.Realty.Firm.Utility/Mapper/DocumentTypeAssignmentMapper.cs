using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;
using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DocumentTypeAssignmentMapper
    {
        public static List<DocumentTypeAssignmentViewModel> ToDocumentTypeAssignmentListViewModel(this IEnumerable<DocumentTypeAssignment> modelList)
        {
            List<DocumentTypeAssignmentViewModel> viewModelList = new List<DocumentTypeAssignmentViewModel>();

            if (modelList != null)
            {
                viewModelList = modelList.Select(o => new DocumentTypeAssignmentViewModel
                {
                    Id = o.Id,
                    DocumentTypeId = o.DocumentTypeId,
                    DocumentType = o.DocumentType,
                    TransactionTypeId = o.TransactionTypeId,
                    TransactionType = o.TransactionType,
                    IsActive = o.IsActive,
                    CreatedBy = o.CreatedBy,
                    CreatedAt = o.CreatedAt,
                    UpdatedBy = o.UpdatedBy,
                    UpdatedAt = o.UpdatedAt
                }).ToList();
            }

            return viewModelList;
        }
        public static DocumentTypeAssignmentViewModel ToDocumentTypeAssignmentViewModel(this DocumentTypeAssignment model)
        {
            return new DocumentTypeAssignmentViewModel
            {
                Id = model.Id,
                DocumentTypeId = model.DocumentTypeId,
                DocumentType = model.DocumentType,
                TransactionTypeId = model.TransactionTypeId,
                TransactionType = model.TransactionType,
            };
        }
        public static CreateDocumentTypeAssignmentViewModel ToCreateDocumentTypeAssignmentViewModel(this DocumentTypeAssignment model)
        {
            return new CreateDocumentTypeAssignmentViewModel
            {
                DocumentTypeId = model.DocumentTypeId,
                TransactionTypeId = model.TransactionTypeId
            };
        }
        public static EditDocumentTypeAssignmentViewModel ToEditDocumentTypeAssignmentViewModel(this DocumentTypeAssignment model)
        {
            return new EditDocumentTypeAssignmentViewModel
            {
                Id = model.Id,
                DocumentTypeId = model.DocumentTypeId,
                TransactionTypeId = model.TransactionTypeId,
            };
        }
    }

}
