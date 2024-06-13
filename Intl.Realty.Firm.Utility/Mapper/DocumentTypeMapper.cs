using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeVM;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DocumentTypeMapper
    {
        public static DocumentTypeViewModel ToDocumentTypeViewModel(this DocumentType model)
        {
            return new DocumentTypeViewModel
            {
                Id = model.Id,
                Name = model.Name,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static DocumentType ToDocumentTypeModel(this CreateDocumentTypeViewModel viewModel)
        {
            return new DocumentType
            {
                Name = viewModel.Name,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
            };
        }
        public static CreateDocumentTypeViewModel ToCreateDocumentTypeViewModel(this DocumentType model)
        {
            return new CreateDocumentTypeViewModel
            {
                Name = model.Name,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt
            };
        }
        public static EditDocumentTypeViewModel ToEditDocumentTypeModel(this DocumentType model)
        {
            return new EditDocumentTypeViewModel
            {
                Id = model.Id,
                Name = model.Name,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static EditDocumentTypeViewModel ToEditDocumentTypeViewModel(this DocumentType model)
        {
            return new EditDocumentTypeViewModel
            {
                Id = model.Id,
                Name = model.Name,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static List<DocumentTypeViewModel> ToDocumentTypeListViewModel(this List<DocumentType> modelList)
        {
            var viewModelList = new List<DocumentTypeViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new DocumentTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelList;
        }
        public static List<DocumentTypeViewModel> ToDocumentTypeListViewModel(this IEnumerable<DocumentType> modelList)
        {
            var viewModelList = new List<DocumentTypeViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new DocumentTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelList;
        }
        public static List<DocumentType> FromIEnumToDocumentTypeList(this IEnumerable<DocumentType> modelIEnum)
        {
            var modelList = new List<DocumentType>();
            if (modelIEnum != null)
            {
                modelList = modelIEnum.Select(x => new DocumentType()
                {
                    Id = x.Id,
                    Name = x.Name,
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
