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
        public static List<UserTypeViewModel> ToUserTypeListViewModel(this IEnumerable<UserType> modelList)
        {
            List<UserTypeViewModel> viewModelList = new List<UserTypeViewModel>();

            if(modelList != null)
            {
                viewModelList = modelList.Select(o => new UserTypeViewModel
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
        public static UserTypeViewModel ToUserTypeViewModel(this UserType model)
        {
            return new UserTypeViewModel
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
        public static UserType ToUserTypeModel(this CreateUserTypeViewModel viewModel)
        {
            return new UserType
            {
                Name = viewModel.Name,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
            };
        }

        public static UserType ToUserTypeModel(this UserTypeViewModel viewModel)
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
