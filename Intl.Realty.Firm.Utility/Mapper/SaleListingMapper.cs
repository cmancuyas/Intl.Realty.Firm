using Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM;
using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class SaleListingMapper
    {
        public static SaleListingViewModel ToSaleListingViewModel(this SaleListing model)
        {
            return new SaleListingViewModel
            {
                Id = model.Id,
                TransactionTypeId = model.TransactionTypeId,
                IRFDealId = model.IRFDealId,
                DealStatusId = model.DealStatusId,
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
                TransactionTypeId = viewModel.TransactionTypeId,
                IRFDealId = viewModel.IRFDealId,
                DealStatusId= viewModel.DealStatusId,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
            };
        }
        public static CreateSaleListingViewModel ToCreateSaleListingViewModel(this SaleListing model)
        {
            return new CreateSaleListingViewModel
            {
                TransactionTypeId = model.TransactionTypeId,
                IRFDealId = model.IRFDealId,
                DealStatusId= model.DealStatusId,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt
            };
        }
        public static EditSaleListingViewModel ToEditSaleListingViewModel(this SaleListing model)
        {
            return new EditSaleListingViewModel
            {
                Id = model.Id,
                TransactionTypeId = model.TransactionTypeId,
                TransactionType = model.TransactionType,
                IRFDealId = model.IRFDealId,
                DealStatusId = model.DealStatusId,
                EditIRFDealViewModel = model.IRFDeal?.ToEditIRFDealModel(),
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy??1, //1 is admin
                UpdatedAt = model.UpdatedAt??DateTime.UtcNow,
            };
        }
        public static List<SaleListingViewModel> ToSaleListingListViewModel(this List<SaleListing> modelList)
        {
            var viewModelList = new List<SaleListingViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new SaleListingViewModel()
                {
                    Id = x.Id,
                    TransactionTypeId = x.TransactionTypeId,
                    IRFDealId = x.IRFDealId,
                    DealStatusId= x.DealStatusId,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelList;
        }
        public static List<SaleListingViewModel> ToSaleListingListViewModel(this IEnumerable<SaleListing> modelList)
        {
            var viewModelList = new List<SaleListingViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new SaleListingViewModel()
                {
                    Id = x.Id,
                    TransactionTypeId = x.TransactionTypeId,
                    TransactionType = x.TransactionType,
                    IRFDealId = x.IRFDealId,
                    IRFDeal = x.IRFDeal,
                    DealStatusId = x.DealStatusId,
                    DealStatus = x.DealStatus,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelList;
        }
        public static List<SaleListing> FromIEnumToSaleListingList(this IEnumerable<SaleListing> modelIEnum)
        {
            var modelList = new List<SaleListing>();

            if (modelIEnum != null)
            {
                modelList = modelIEnum.Select(x => new SaleListing()
                {
                    Id = x.Id,
                    TransactionTypeId = x.TransactionTypeId,
                    IRFDealId = x.IRFDealId,
                    DealStatusId = x.DealStatusId,
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
