using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.ProvinceVM;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class ProvinceMapper
    {
        public static Province FromCreateToProvinceModel(this CreateProvinceViewModel viewModel)
        {
            return new Province
            {
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                CreatedAt = viewModel.CreatedAt,
                CreatedBy = viewModel.CreatedBy,
            };
        }
        public static Province FromEditToProvinceModel(this EditProvinceViewModel viewModel)
        {
            return new Province
            {
                Id = viewModel.Id,
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt,
            };
        }
        public static EditProvinceViewModel ToEditProvinceListViewModel(this Province model)
        {
            return new EditProvinceViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }

        public static List<Province> FromModelToProvinceListModel(this Province model)
        {
            var modelList = new List<Province>();
            if (model != null)
            {
                modelList.Add(model);
            }
            return modelList;
        }

        public static List<ProvinceViewModel> ToProvinceListViewModel(this List<Province> modelList)
        {
            var viewModelList = new List<ProvinceViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new ProvinceViewModel()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelList;
        }


        public static IEnumerable<ProvinceViewModel> FromIEnumToProvinceIEnumViewModel(this IEnumerable<Province> modelIEnum)
        {
            IEnumerable<ProvinceViewModel> viewModelIEnum = new List<ProvinceViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new ProvinceViewModel()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Description = x.Description,
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
