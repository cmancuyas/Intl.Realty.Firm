using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.RoleVM;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class RoleMapper
    {
        public static IEnumerable<DeleteRoleViewModel> ToDeleteRoleIEnumViewModel(this IEnumerable<Role> modelIEnum)
        {
            IEnumerable<DeleteRoleViewModel> viewModelIEnum = new List<DeleteRoleViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DeleteRoleViewModel()
                {
                    Id = x.Id,
                });
            }
            return viewModelIEnum;
        }
        public static EditRoleViewModel ToEditRoleViewModel(this Role model)
        {
            return new EditRoleViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description ?? "",
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static IEnumerable<RoleViewModel> ToRoleIEnumViewModel(this IEnumerable<Role> modelIEnum)
        {
            IEnumerable<RoleViewModel> viewModelIEnum = new List<RoleViewModel>();

            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new RoleViewModel()
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
        public static Role ToRoleModel(this CreateRoleViewModel viewModel)
        {
            var model = new Role();
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.IsActive = viewModel.IsActive;
            model.CreatedAt = viewModel.CreatedAt;
            model.CreatedBy = viewModel.CreatedBy;
            return model;
        }
        public static Role ToRoleModel(this EditRoleViewModel viewModel)
        {
            return new Role
            {
                Id = viewModel.Id,
                Code = viewModel.Code,
                Description = viewModel.Description ?? "",
                IsActive = viewModel.IsActive,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt
            };
        }
    }
}