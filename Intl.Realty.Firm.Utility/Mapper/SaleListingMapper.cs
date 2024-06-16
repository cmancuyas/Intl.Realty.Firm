using Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM;
using Intl.Realty.Firm.Models.Models;
using System.Reflection;
using Intl.Realty.Firm.Models.Models.ViewModel.IRFDealVM;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class SaleListingMapper
    {
        public static List<SaleListingViewModel> ToSaleListingListViewModel(this IEnumerable<SaleListing> modelList)
        {
            List<SaleListingViewModel> viewModelList = new List<SaleListingViewModel>();

            if (modelList != null)
            {
                viewModelList = modelList.Select(o => new SaleListingViewModel
                {
                    Id = o.Id,
                    PropertyAddress = o.PropertyAddress,
                    TransactionTypeId = o.TransactionTypeId,
                    IRFDealId = o.IRFDealId,
                    IsActive = o.IsActive,
                    CreatedBy = o.CreatedBy,
                    CreatedAt = o.CreatedAt,
                    UpdatedBy = o.UpdatedBy,
                    UpdatedAt = o.UpdatedAt

                }).ToList();
            }

            return viewModelList;
        }
        public static SaleListingViewModel ToSaleListingViewModel(this SaleListing model)
        {
            return new SaleListingViewModel
            {
                Id = model.Id,
                PropertyAddress = model.PropertyAddress,
                TransactionTypeId = model.TransactionTypeId,
                IRFDealId = model.IRFDealId,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static SaleListing ToSaleListingModel(this CreateSaleListingViewModel viewModel)
        {
            return new SaleListing
            {
                PropertyAddress = viewModel.PropertyAddress,
                TransactionTypeId = viewModel.TransactionTypeId,
                IRFDealId = viewModel.IRFDealId,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt
            };
        }

        public static SaleListing ToSaleListingModel(this SaleListingViewModel viewModel)
        {
            return new SaleListing
            {
                Id = viewModel.Id,
                PropertyAddress = viewModel.PropertyAddress,
                TransactionTypeId = viewModel.TransactionTypeId,
                IRFDealId = viewModel.IRFDealId,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt

            };
        }
        public static EditSaleListingViewModel ToEditSaleListingModel(this SaleListing model)
        {
            return new EditSaleListingViewModel
            {
                Id = model.Id,
                PropertyAddress = model.PropertyAddress,
                TransactionTypeId = model.TransactionTypeId,
                IRFDealId = model.IRFDealId,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }


    }
}
