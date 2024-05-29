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
        public static CreateUserTypeViewModel ToCreateUserTypeViewModel(this UserType model)
        {
            return new CreateUserTypeViewModel
            {
                Name = model.Name,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
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
    }
}
