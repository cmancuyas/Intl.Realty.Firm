using Intl.Realty.Firm.Models.Models.ViewModel.ProvinceVM;
using Intl.Realty.Firm.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class ProvinceMapper
    {
        public static List<ProvinceViewModel> ToProvinceListViewModel(this IEnumerable<Province> modelList)
        {
            List<ProvinceViewModel> viewModelList = new List<ProvinceViewModel>();

            if(modelList != null)
            {
                viewModelList = modelList.Select(o => new ProvinceViewModel
                {
                    Id = o.Id,
                    Code = o.Code,
                    Description = o.Description,
                    IsActive = o.IsActive,
                    CreatedBy = o.CreatedBy,
                    CreatedAt = o.CreatedAt,
                    UpdatedBy = o.UpdatedBy,
                    UpdatedAt = o.UpdatedAt
                }).ToList();
            }

            return viewModelList;
        }
        public static ProvinceViewModel ToProvinceViewModel(this Province model)
        {
            return new ProvinceViewModel
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
        public static Province ToProvinceModel(this CreateProvinceViewModel viewModel)
        {
            return new Province
            {
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
            };
        }

        public static Province ToProvinceModel(this ProvinceViewModel viewModel)
        {
            return new Province
            {
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt
            };
        }
        public static EditProvinceViewModel ToEditProvinceModel(this Province model)
        {
            return new EditProvinceViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}
