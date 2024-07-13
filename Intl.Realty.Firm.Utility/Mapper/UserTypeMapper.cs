using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class UserTypeMapper
    {
        public static UserType FromCreateToUserTypeModel(this CreateUserTypeViewModel viewModel)
        {
            return new UserType
            {
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                CreatedAt = viewModel.CreatedAt,
                CreatedBy = viewModel.CreatedBy,
            };
        }
        public static UserType FromEditToUserTypeModel(this EditUserTypeViewModel viewModel)
        {
            return new UserType
            {
                Id = viewModel.Id,
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt,
            };
        }
        public static EditUserTypeViewModel ToEditUserTypeListViewModel(this UserType model)
        {
            return new EditUserTypeViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }

        public static List<UserType> FromModelToUserTypeListModel(this UserType model)
        {
            var modelList = new List<UserType>();
            if (model != null)
            {
                modelList.Add(model);
            }
            return modelList;
        }

        public static List<UserTypeViewModel> ToUserTypeListViewModel(this List<UserType> modelList)
        {
            var viewModelList = new List<UserTypeViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new UserTypeViewModel()
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


        public static IEnumerable<UserTypeViewModel> FromIEnumToUserTypeIEnumViewModel(this IEnumerable<UserType> modelIEnum)
        {
            IEnumerable<UserTypeViewModel> viewModelIEnum = new List<UserTypeViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new UserTypeViewModel()
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
