using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
using Intl.Realty.Firm.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class UserTypeMapper
    {
        public static List<SaleListingViewModel> ToUserTypeListViewModel(this IEnumerable<UserType> modelList)
        {
            List<SaleListingViewModel> viewModelList = new List<SaleListingViewModel>();

            if(modelList != null)
            {
                viewModelList = modelList.Select(o => new SaleListingViewModel
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
        public static SaleListingViewModel ToUserTypeViewModel(this UserType model)
        {
            return new SaleListingViewModel
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
        public static UserType ToUserTypeModel(this CreateSaleListingViewModel viewModel)
        {
            return new UserType
            {
                Name = viewModel.Name,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
            };
        }

        public static UserType ToUserTypeModel(this SaleListingViewModel viewModel)
        {
            return new UserType
            {
                Name = viewModel.Name,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt
            };
        }
        public static EditUserTypeViewModel ToEditUserTypeModel(this UserType model)
        {
            return new EditUserTypeViewModel
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
