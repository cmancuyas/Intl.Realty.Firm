using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DocumentTypeAssignmentMapper
    {
        public static IEnumerable<DeleteDocumentTypeAssignmentViewModel> ToDeleteDocumentTypeAssignmentIEnumViewModel(this IEnumerable<DocumentTypeAssignment> modelIEnum)
        {
            IEnumerable<DeleteDocumentTypeAssignmentViewModel> viewModelIEnum = new List<DeleteDocumentTypeAssignmentViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DeleteDocumentTypeAssignmentViewModel()
                {
                    Id = x.Id,
                });
            }
            return viewModelIEnum;
        }
        public static EditDocumentTypeAssignmentViewModel ToEditDocumentTypeAssignmentViewModel(this DocumentTypeAssignment model)
        {
            return new EditDocumentTypeAssignmentViewModel
            {
                Id = model.Id,
                DocumentTypeId = model.DocumentTypeId,
                TransactionTypeId = model.TransactionTypeId,
                IsActive = model.IsActive,
                UpdatedAt = model.UpdatedAt,
                UpdatedBy = model.UpdatedBy,
            };
        }
        public static DocumentTypeAssignment ToDocumentTypeAssignment(this CreateDocumentTypeAssignmentViewModel viewModel)
        {
            return new DocumentTypeAssignment
            {
                DocumentTypeId = viewModel.DocumentTypeId,
                TransactionTypeId = viewModel.TransactionTypeId,
                IsActive = viewModel.IsActive,
                CreatedAt = viewModel.CreatedAt,
                CreatedBy = viewModel.CreatedBy,
            };
        }
        public static DocumentTypeAssignment ToDocumentTypeAssignment(this EditDocumentTypeAssignmentViewModel viewModel)
        {
            return new DocumentTypeAssignment
            {
                Id = viewModel.Id,
                DocumentTypeId = viewModel.DocumentTypeId,
                TransactionTypeId = viewModel.TransactionTypeId,
                IsActive = viewModel.IsActive,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt,
            };
        }

        public static IEnumerable<DocumentTypeAssignmentViewModel> ToDocumentTypeAssignmentIEnumViewModel(this IEnumerable<DocumentTypeAssignment> modelIEnum)
        {
            IEnumerable<DocumentTypeAssignmentViewModel> viewModelIEnum = new List<DocumentTypeAssignmentViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DocumentTypeAssignmentViewModel()
                {
                    Id = x.Id,
                    DocumentTypeId = x.DocumentTypeId,
                    DocumentType = x.DocumentType,
                    TransactionTypeId = x.TransactionTypeId,
                    TransactionType = x.TransactionType,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelIEnum;
        }
        
    }
}
