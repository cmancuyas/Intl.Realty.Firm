using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DepartmentVM;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DepartmentMapper
    {
        public static Department FromCreateToDepartmentModel(this CreateDepartmentViewModel viewModel)
        {
            return new Department
            {
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                CreatedAt = viewModel.CreatedAt,
                CreatedBy = viewModel.CreatedBy,
            };
        }
        public static Department FromEditToDepartmentModel(this EditDepartmentViewModel viewModel)
        {
            return new Department
            {
                Id = viewModel.Id,
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt,
            };
        }
        public static EditDepartmentViewModel ToEditDepartmentListViewModel(this Department model)
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

        public static List<Department> FromModelToDepartmentListModel(this Department model)
        {
            var modelList = new List<Department>();
            if (model != null)
            {
                modelList.Add(model);
            }
            return modelList;
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


        public static IEnumerable<DepartmentViewModel> FromIEnumToDepartmentIEnumViewModel(this IEnumerable<Department> modelIEnum)
        {
            IEnumerable<DepartmentViewModel> viewModelIEnum = new List<DepartmentViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DepartmentViewModel()
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
