using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.EmploymentStatusVM;
using System.Collections.Generic;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class EmploymentStatusMapper
    {
        public static IEnumerable<DeleteEmploymentStatusViewModel> ToDeleteEmploymentStatusIEnumViewModel(this IEnumerable<EmploymentStatus> modelIEnum)
        {
            IEnumerable<DeleteEmploymentStatusViewModel> viewModelIEnum = new List<DeleteEmploymentStatusViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DeleteEmploymentStatusViewModel()
                {
                    Id = x.Id,
                });
            }
            return viewModelIEnum;
        }
        public static EditEmploymentStatusViewModel ToEditEmploymentStatusViewModel(this EmploymentStatus model)
        {
            return new EditEmploymentStatusViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static IEnumerable<EmploymentStatusViewModel> ToEmploymentStatusIEnumViewModel(this IEnumerable<EmploymentStatus> modelIEnum)
        {
            IEnumerable<EmploymentStatusViewModel> viewModelIEnum = new List<EmploymentStatusViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new EmploymentStatusViewModel()
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
        public static EmploymentStatus ToEmploymentStatusModel(this CreateEmploymentStatusViewModel viewModel)
        {
            var model = new EmploymentStatus();
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.IsActive = viewModel.IsActive;
            model.CreatedAt = viewModel.CreatedAt;
            model.CreatedBy = viewModel.CreatedBy;
            return model;
        }
        public static EmploymentStatus ToEmploymentStatusModel(this EditEmploymentStatusViewModel viewModel)
        {
            return new EmploymentStatus 
            {
                Id = viewModel.Id,
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt
            };
        }
        public static List<EmploymentStatusExport> ToEmploymentStatusListExport(this List<EmploymentStatus> modelList)
        {
            List<EmploymentStatusExport> exportList = new List<EmploymentStatusExport>();

            if(modelList != null)
            {
                exportList = modelList.Select(x => new EmploymentStatusExport
                {
                    Id = x.Id,
                    Code = x.Code,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt
                }).ToList();
            }
            return exportList;
        }
    }
}