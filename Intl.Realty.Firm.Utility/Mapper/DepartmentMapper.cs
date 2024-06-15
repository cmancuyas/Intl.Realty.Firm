using Intl.Realty.Firm.Models.Models.ViewModel.DepartmentVM;
using Intl.Realty.Firm.Models.Models;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DepartmentMapper
    {
        public static DepartmentViewModel ToDepartmentViewModel(this Department model)
        {
            return new DepartmentViewModel
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
        public static Department ToDepartmentModel(this CreateDepartmentViewModel viewModel)
        {
            return new Department
            {
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
            };
        }
        public static CreateDepartmentViewModel ToCreateDepartmentViewModel(this Department model)
        {
            return new CreateDepartmentViewModel
            {
                Code = model.Code,
                Description = model.Description,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt
            };
        }
        public static EditDepartmentViewModel ToEditDepartmentModel(this Department model)
        {
            return new EditDepartmentViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static EditDepartmentViewModel ToEditDepartmentViewModel(this Department model)
        {
            return new EditDepartmentViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static List<DepartmentViewModel> ToDepartmentListViewModel(this List<Department> modelList)
        {
            var viewModelList = new List<DepartmentViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new DepartmentViewModel()
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
        public static List<DepartmentViewModel> ToDepartmentListViewModel(this IEnumerable<Department> modelList)
        {
            var viewModelList = new List<DepartmentViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new DepartmentViewModel()
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
        public static List<Department> FromIEnumToDepartmentList(this IEnumerable<Department> modelIEnum)
        {
            var modelList = new List<Department>();
            if (modelIEnum != null)
            {
                modelList = modelIEnum.Select(x => new Department()
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
