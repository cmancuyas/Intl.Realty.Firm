using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeVM;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DocumentTypeMapper
    {
        public static DocumentType FromCreateToDocumentTypeModel(this CreateDocumentTypeViewModel viewModel)
        {
            return new DocumentType
            {
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsRequired = viewModel.IsRequired,
                IsActive = viewModel.IsActive,
                CreatedAt = viewModel.CreatedAt,
                CreatedBy = viewModel.CreatedBy,
            };
        }
        public static DocumentType FromEditToDocumentTypeModel(this EditDocumentTypeViewModel viewModel)
        {
            return new DocumentType
            {
                Id = viewModel.Id,
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsRequired = viewModel.IsRequired,
                IsActive = viewModel.IsActive,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt,
            };
        }
        public static EditDocumentTypeViewModel ToEditDocumentTypeListViewModel(this DocumentType model)
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

        public static List<DocumentType> FromModelToDocumentTypeListModel(this DocumentType model)
        {
            var modelList = new List<DocumentType>();
            if (model != null)
            {
                modelList.Add(model);
            }
            return modelList;
        }

        public static List<DocumentTypeViewModel> ToDocumentTypeListViewModel(this List<DocumentType> modelList)
        {
            var viewModelList = new List<DocumentTypeViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new DocumentTypeViewModel()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Description = x.Description,
                    IsRequired = x.IsRequired,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelList;
        }


        public static IEnumerable<DocumentTypeViewModel> FromIEnumToDocumentTypeIEnumViewModel(this IEnumerable<DocumentType> modelIEnum)
        {
            IEnumerable<DocumentTypeViewModel> viewModelIEnum = new List<DocumentTypeViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DocumentTypeViewModel()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Description = x.Description,
                    IsRequired = x.IsRequired,
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
