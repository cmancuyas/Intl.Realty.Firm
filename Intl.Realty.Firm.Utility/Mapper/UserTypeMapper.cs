using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
using Intl.Realty.Firm.Models.Models;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class UserTypeMapper
    {
        public static UserTypeViewModel ToUserTypeViewModel(this UserType model)
        {
            return new UserTypeViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static UserType ToUserTypeModel(this CreateUserTypeViewModel viewModel)
        {
            return new UserType
            {
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
            };
        }
        public static CreateUserTypeViewModel ToCreateUserTypeViewModel(this UserType model)
        {
            return new CreateUserTypeViewModel
            {
                Code = model.Code,
                Description = model.Description,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt
            };
        }
        public static EditUserTypeViewModel ToEditUserTypeModel(this UserType model)
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
        public static EditUserTypeViewModel ToEditUserTypeViewModel(this UserType model)
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
        public static List<UserTypeViewModel> ToUserTypeListViewModel(this IEnumerable<UserType> modelList)
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
        public static List<UserType> FromIEnumToUserTypeList(this IEnumerable<UserType> modelIEnum)
        {
            var modelList = new List<UserType>();
            if (modelIEnum != null)
            {
                modelList = modelIEnum.Select(x => new UserType()
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
            return modelList;
        }
    }
}
