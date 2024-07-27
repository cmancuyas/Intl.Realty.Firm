using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class UserTypeMapper
    {
        public static IEnumerable<DeleteUserTypeViewModel> ToDeleteUserTypeIEnumViewModel(this IEnumerable<UserType> modelIEnum)
        {
            IEnumerable<DeleteUserTypeViewModel> viewModelIEnum = new List<DeleteUserTypeViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DeleteUserTypeViewModel()
                {
                    Id = x.Id,
                });
            }
            return viewModelIEnum;
        }
        public static IEnumerable<UserTypeViewModel> ToUserTypeIEnumViewModel(this IEnumerable<UserType> modelIEnum)
        {
            IEnumerable<UserTypeViewModel> viewModelIEnum = new List<UserTypeViewModel>();
            if (viewModelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new UserTypeViewModel()
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
        public static EditUserTypeViewModel ToEditUserTypeViewModel(this UserType model)
        {
            return new EditUserTypeViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description ?? "",
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static UserType ToUserTypeModel(this CreateUserTypeViewModel viewModel)
        {
            var model = new UserType();
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.IsActive = viewModel.IsActive;
            model.CreatedAt = viewModel.CreatedAt;
            model.CreatedBy = viewModel.CreatedBy;
            return model;
        }
        public static UserType ToUserTypeModel(this EditUserTypeViewModel viewModel)
        {
            var model = new UserType();
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.IsActive = viewModel.IsActive;
            model.UpdatedAt = viewModel.UpdatedAt;
            model.UpdatedBy = viewModel.UpdatedBy;
            return model;
        }
    }
}
