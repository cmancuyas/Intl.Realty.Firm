using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DocumentTypeAssignmentMapper
    {
        public static DocumentTypeAssignment FromCreateToDocumentTypeAssignmentModel(this CreateDocumentTypeAssignmentViewModel viewModel)
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
        public static DocumentTypeAssignment FromEditToDocumentTypeAssignmentModel(this EditDocumentTypeAssignmentViewModel viewModel)
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
        public static EditDocumentTypeAssignmentViewModel ToEditDocumentTypeAssignmentListViewModel(this DocumentTypeAssignment model)
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

        public static List<DocumentTypeAssignment> FromModelToDocumentTypeAssignmentListModel(this DocumentTypeAssignment model)
        {
            var modelList = new List<DocumentTypeAssignment>();
            if (model != null)
            {
                modelList.Add(model);
            }
            return modelList;
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


        public static IEnumerable<DocumentTypeAssignmentViewModel> FromIEnumToDocumentTypeAssignmentIEnumViewModel(this IEnumerable<DocumentTypeAssignment> modelIEnum)
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
