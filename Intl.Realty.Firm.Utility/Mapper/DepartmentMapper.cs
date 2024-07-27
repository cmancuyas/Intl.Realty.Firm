using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DepartmentVM;
using Intl.Realty.Firm.Models.Models.ViewModel.DepartmentVM;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DepartmentMapper
    {
        public static IEnumerable<DeleteDepartmentViewModel> ToDeleteDepartmentIEnumViewModel(this IEnumerable<Department> modelIEnum)
        {
            IEnumerable<DeleteDepartmentViewModel> viewModelIEnum = new List<DeleteDepartmentViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DeleteDepartmentViewModel()
                {
                    Id = x.Id,
                });
            }
            return viewModelIEnum;
        }
        public static EditDepartmentViewModel ToEditDepartmentViewModel(this Department model)
        {
            return new EditDepartmentViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description ?? "",
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static IEnumerable<DepartmentViewModel> ToDepartmentIEnumViewModel(this IEnumerable<Department> modelIEnum)
        {
            IEnumerable<DepartmentViewModel> viewModelIEnum = new List<DepartmentViewModel>();
            if (viewModelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DepartmentViewModel()
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
        public static Department ToDepartmentModel(this CreateDepartmentViewModel viewModel)
        {
            var model = new Department();
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.IsActive = viewModel.IsActive;
            model.CreatedAt = viewModel.CreatedAt;
            model.CreatedBy = viewModel.CreatedBy;
            return model;
        }
        public static Department ToDepartmentModel(this EditDepartmentViewModel viewModel)
        {
            var model = new Department();
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.IsActive = viewModel.IsActive;
            model.UpdatedAt = viewModel.UpdatedAt;
            model.UpdatedBy = viewModel.UpdatedBy;
            return model;
        }
    }
}