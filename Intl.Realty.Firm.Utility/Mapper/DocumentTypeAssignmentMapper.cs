using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DocumentTypeAssignmentAssignmentMapper
    {
        public static DocumentTypeAssignmentViewModel ToDocumentTypeAssignmentViewModel(this DocumentTypeAssignment model)
        {
            return new DocumentTypeAssignmentViewModel
            {
                Id = model.Id,
                DocumentTypeId = model.DocumentTypeId,
                DocumentType = model.DocumentType,
                TransactionTypeId = model.TransactionTypeId,
                TransactionType = model.TransactionType,
                IsActive = model.IsActive,
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
                CreatedAt = viewModel.CreatedAt,
            };
        }
        public static CreateDocumentTypeAssignmentViewModel ToCreateDocumentTypeAssignmentViewModel(this DocumentTypeAssignment model)
        {
            return new CreateDocumentTypeAssignmentViewModel
            {
                DocumentTypeId = model.DocumentTypeId,
                TransactionTypeId = model.TransactionTypeId,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt
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
        public static EditDocumentTypeAssignmentViewModel ToEditDocumentTypeAssignmentViewModel(this DocumentTypeAssignment model)
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
        public static List<DocumentTypeAssignmentViewModel> ToDocumentTypeAssignmentListViewModel(this List<DocumentTypeAssignment> modelList)
        {
            var viewModelList = new List<DocumentTypeAssignmentViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new DocumentTypeAssignmentViewModel()
                {
                    Id = x.Id,
                    DocumentTypeId = x.DocumentTypeId,
                    TransactionTypeId = x.TransactionTypeId,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelList;
        }
        public static List<DocumentTypeAssignmentViewModel> ToDocumentTypeAssignmentListViewModel(this IEnumerable<DocumentTypeAssignment> modelList)
        {
            var viewModelList = new List<DocumentTypeAssignmentViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new DocumentTypeAssignmentViewModel()
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
            return viewModelList;
        }
        public static List<DocumentTypeAssignment> FromIEnumToDocumentTypeAssignmentList(this IEnumerable<DocumentTypeAssignment> modelIEnum)
        {
            var modelList = new List<DocumentTypeAssignment>();
            if (modelIEnum != null)
            {
                modelList = modelIEnum.Select(x => new DocumentTypeAssignment()
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
            return modelList;
        }
    }

}
