using Intl.Realty.Firm.Models.Models.ViewModel.LeaseListingVM;
using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class LeaseListingMapper
    {
        public static LeaseListingViewModel ToLeaseListingViewModel(this LeaseListing model)
        {
            return new LeaseListingViewModel
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
        public static LeaseListing ToLeaseListingModel(this CreateLeaseListingViewModel viewModel)
        {
            return new LeaseListing
            {
                TransactionTypeId = viewModel.TransactionTypeId,
                IRFDealId = viewModel.IRFDealId,
                DealStatusId= viewModel.DealStatusId,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
            };
        }
        public static CreateLeaseListingViewModel ToCreateLeaseListingViewModel(this LeaseListing model)
        {
            return new CreateLeaseListingViewModel
            {
                TransactionTypeId = model.TransactionTypeId,
                IRFDealId = model.IRFDealId,
                DealStatusId= model.DealStatusId,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt
            };
        }
        public static EditLeaseListingViewModel ToEditLeaseListingViewModel(this LeaseListing model)
        {
            return new EditLeaseListingViewModel
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
        public static List<LeaseListingViewModel> ToLeaseListingListViewModel(this List<LeaseListing> modelList)
        {
            var viewModelList = new List<LeaseListingViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new LeaseListingViewModel()
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
        public static List<LeaseListingViewModel> ToLeaseListingListViewModel(this IEnumerable<LeaseListing> modelList)
        {
            var viewModelList = new List<LeaseListingViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new LeaseListingViewModel()
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
        public static List<LeaseListingExport> ToLeaseListingListExport(this List<LeaseListing> modelList)
        {
            List<LeaseListingExport> exportList = new List<LeaseListingExport>();

            if (modelList != null)
            {
                exportList = modelList.Select(x => new LeaseListingExport
                {
                    TransactionType = x.TransactionType!.Description,
                    PropertyAddress = x.IRFDeal!.PropertyAddress,
                    FinalSalePrice = x.IRFDeal!.PropertyAddress,
                    FinalClosingDate = x.IRFDeal!.FinalClosingDate,
                    DepositAmount = x.IRFDeal!.DepositAmount.ToString(),
                    DepositDate = x.IRFDeal!.DepositDate,
                    BuyerName = x.IRFDeal!.BuyerName,
                    LandLordName = x.IRFDeal!.LandLordName,
                    ListingCommissionPercentage = x.IRFDeal!.ListingCommissionPercentage.ToString(),
                    BuyingCommissionPercentage = x.IRFDeal!.BuyingCommissionPercentage.ToString(),
                    ListingAgentName = x.IRFDeal!.ListingAgentName,
                    ListingBrokerage = x.IRFDeal!.ListingBrokerage,
                    ListingBrokerageFax = x.IRFDeal!.ListingBrokerageFax,
                    BuyerAgentName = x.IRFDeal!.BuyerAgentName,
                    BuyerBrokerage = x.IRFDeal!.BuyerBrokerage,
                    BuyerBrokerageFax = x.IRFDeal!.BuyerBrokerageFax,
                    SellersLawyer = x.IRFDeal!.SellersLawyer,
                    SellersLawyerAddress = x.IRFDeal!.SellersLawyerAddress,
                    SellersPhoneNumber = x.IRFDeal!.SellersPhoneNumber,
                    BuyersLawyer = x.IRFDeal!.BuyersLawyer,
                    BuyersLawyerAddress = x.IRFDeal!.BuyersLawyerAddress,
                    BuyersPhoneNumber = x.IRFDeal!.BuyersPhoneNumber,
                    DealStatus = x.DealStatus!.Description,

                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,

                }).ToList();
            }
            return exportList;
        }
    }
}
