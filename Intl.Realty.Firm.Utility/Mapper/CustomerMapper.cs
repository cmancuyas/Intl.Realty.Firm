using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.CustomerVM;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class CustomerMapper
    {
        public static IEnumerable<DeleteCustomerViewModel> ToDeleteCustomerIEnumViewModel(this IEnumerable<Customer> modelIEnum)
        {
            IEnumerable<DeleteCustomerViewModel> viewModelIEnum = new List<DeleteCustomerViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DeleteCustomerViewModel()
                {
                    Id = x.Id,
                });
            }
            return viewModelIEnum;
        }
        public static EditCustomerViewModel ToEditCustomerViewModel(this Customer model)
        {
            return new EditCustomerViewModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static IEnumerable<CustomerViewModel> ToCustomerIEnumViewModel(this IEnumerable<Customer> modelIEnum)
        {
            IEnumerable<CustomerViewModel> viewModelIEnum = new List<CustomerViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new CustomerViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Address = x.Address,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                });
            }
            return viewModelIEnum!;
        }
        public static Customer ToCustomerModel(this CreateCustomerViewModel viewModel)
        {
            var model = new Customer();
            model.FirstName = viewModel.FirstName;
            model.LastName = viewModel.LastName;
            model.Email = viewModel.Email;
            model.Address = viewModel.Address;
            model.IsActive = viewModel.IsActive;
            model.CreatedAt = viewModel.CreatedAt;
            model.CreatedBy = viewModel.CreatedBy;
            return model;
        }
        public static Customer ToCustomerModel(this EditCustomerViewModel viewModel)
        {
            return new Customer
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Address = viewModel.Address,
                IsActive = viewModel.IsActive,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt
            };
        }
    }
}