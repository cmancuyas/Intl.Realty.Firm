using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.ProvinceVM;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class ProvinceMapper
    {
        public static IEnumerable<DeleteProvinceViewModel> ToDeleteProvinceIEnumViewModel(this IEnumerable<Province> modelIEnum)
        {
            IEnumerable<DeleteProvinceViewModel> viewModelIEnum = new List<DeleteProvinceViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DeleteProvinceViewModel()
                {
                    Id = x.Id,
                });
            }
            return viewModelIEnum;
        }
        public static IEnumerable<ProvinceViewModel> ToProvinceIEnumViewModel(this IEnumerable<Province> modelIEnum)
        {
            IEnumerable<ProvinceViewModel> viewModelIEnum = new List<ProvinceViewModel>();
            if (viewModelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new ProvinceViewModel()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                });
            }
            return viewModelIEnum!;
        }
        public static EditProvinceViewModel ToEditProvinceViewModel(this Province model)
        {
            return new EditProvinceViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description ?? "",
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static Province ToProvinceModel(this CreateProvinceViewModel viewModel)
        {
            var model = new Province();
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.IsActive = viewModel.IsActive;
            model.CreatedAt = viewModel.CreatedAt;
            model.CreatedBy = viewModel.CreatedBy;
            return model;
        }
        public static Province ToProvinceModel(this EditProvinceViewModel viewModel)
        {
            var model = new Province();
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.IsActive = viewModel.IsActive;
            model.UpdatedAt = viewModel.UpdatedAt;
            model.UpdatedBy = viewModel.UpdatedBy;
            return model;
        }

    }
}
