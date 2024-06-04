using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeVM;
using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

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
                IsActive = model.IsActive,
                TransactionTypeId = model.TransactionTypeId,
                TransactionType = model.TransactionType,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
    };
        }
        public static DocumentTypeAssignment ToDocumentTypeAssignmentModel(this CreateDocumentTypeAssignmentViewModel viewModel)
        {
            return new DocumentTypeAssignment
            {
                DocumentTypeId = viewModel.DocumentTypeId,
                TransactionTypeId = viewModel.TransactionTypeId,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt
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
        public static EditDocumentTypeAssignmentViewModel ToEditDocumentTypeAssignmentModel(this DocumentTypeAssignment model)
        {
            return new EditDocumentTypeAssignmentViewModel
            {
                Id = model.Id,
                DocumentTypeId = model.DocumentTypeId,
                TransactionTypeId = model.TransactionTypeId,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt

            };
        }
    }

}
