using Intl.Realty.Firm.Models.Models.ViewModel.DepartmentVM;
using Intl.Realty.Firm.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DepartmentMapper
    {
        public static List<DepartmentViewModel> ToDepartmentListViewModel(this IEnumerable<Department> modelList)
        {
            List<DepartmentViewModel> viewModelList = new List<DepartmentViewModel>();

            if(modelList != null)
            {
                viewModelList = modelList.Select(o => new DepartmentViewModel
                {
                    Id = o.Id,
                    Name = o.Name,
                    IsActive = o.IsActive,
                    CreatedBy = o.CreatedBy,
                    CreatedAt = o.CreatedAt,
                    UpdatedBy = o.UpdatedBy,
                    UpdatedAt = o.UpdatedAt
                }).ToList();
            }

            return viewModelList;
        }
        public static DepartmentViewModel ToDepartmentViewModel(this Department model)
        {
            return new DepartmentViewModel
            {
                Id = model.Id,
                Name = model.Name,
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
                Name = viewModel.Name,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
            };
        }

        public static Department ToDepartmentModel(this DepartmentViewModel viewModel)
        {
            return new Department
            {
                Name = viewModel.Name,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt
            };
        }
        public static EditDepartmentViewModel ToEditDepartmentModel(this Department model)
        {
            return new EditDepartmentViewModel
            {
                Id = model.Id,
                Name = model.Name,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}
