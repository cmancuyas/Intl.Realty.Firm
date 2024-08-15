using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeVM;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DocumentTypeMapper
    {
        public static IEnumerable<DeleteDocumentTypeViewModel> ToDeleteDocumentTypeIEnumViewModel(this IEnumerable<DocumentType> modelIEnum)
        {
            IEnumerable<DeleteDocumentTypeViewModel> viewModelIEnum = new List<DeleteDocumentTypeViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DeleteDocumentTypeViewModel()
                {
                    Id = x.Id,
                });
            }
            return viewModelIEnum;
        }
        public static IEnumerable<DocumentTypeViewModel> ToDocumentTypeIEnumViewModel(this IEnumerable<DocumentType> modelIEnum)
        {
            IEnumerable<DocumentTypeViewModel> viewModelIEnum = new List<DocumentTypeViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DocumentTypeViewModel()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    IsRequired = x.IsRequired,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                });
            }
            return viewModelIEnum!;
        }
        public static EditDocumentTypeViewModel ToEditDocumentTypeViewModel(this DocumentType model)
        {
            return new EditDocumentTypeViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                IsRequired = model.IsRequired,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static DocumentType ToDocumentTypeModel(this CreateDocumentTypeViewModel viewModel)
        {
            var model = new DocumentType();
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.IsRequired = viewModel.IsRequired;
            model.IsActive = viewModel.IsActive;
            model.CreatedAt = viewModel.CreatedAt;
            model.CreatedBy = viewModel.CreatedBy;
            return model;
        }
        public static DocumentType ToDocumentTypeModel(this EditDocumentTypeViewModel viewModel)
        {
            return new DocumentType
            {
                Id = viewModel.Id,
                Code = viewModel.Code,
                Description = viewModel.Description ?? "",
                IsRequired = viewModel.IsRequired,
                IsActive = viewModel.IsActive,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt
            };
        }

    }
}
