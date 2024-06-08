using Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM;
using Intl.Realty.Firm.Models.Models;

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
                    TransactionTypeId = o.TransactionTypeId,
                    DocumentTypeAssignmentId = o.DocumentTypeAssignmentId,
                    DocumentTypeAssignment = o.DocumentTypeAssignment,
                    PropertyAddress = o.PropertyAddress,
                    FinalSalePrice = o.FinalSalePrice,
                    FinalClosingDate = o.FinalClosingDate,
                    DepositAmount = o.DepositAmount,
                    DepositDate = o.DepositDate,
                    BuyerName = o.BuyerName,
                    LandLordName = o.LandLordName,
                    ListingCommissionPercentage = o.ListingCommissionPercentage,
                    BuyingCommissionPercentage = o.BuyingCommissionPercentage,
                    ListingAgentName = o.ListingAgentName,
                    ListingBrokerage = o.ListingBrokerage,
                    ListingBrokerageFax = o.ListingBrokerageFax,
                    BuyerAgentName = o.BuyerAgentName,
                    BuyerBrokerage = o.BuyerBrokerage,
                    BuyerBrokerageFax = o.BuyerBrokerageFax,
                    SellersLawyer = o.SellersLawyer,
                    SellersLawyerAddress = o.SellersLawyerAddress,
                    SellersPhoneNumber = o.SellersPhoneNumber,
                    BuyersLawyer = o.BuyersLawyer,
                    BuyersLawyerAddress = o.BuyersLawyerAddress
                }).ToList();
            }

            return viewModelList;
        }
        public static SaleListingViewModel ToSaleListingViewModel(this SaleListing model)
        {
            return new SaleListingViewModel
            {
                Id = model.Id,
                TransactionTypeId = model.TransactionTypeId,
                DocumentTypeAssignmentId = model.DocumentTypeAssignmentId,
                DocumentTypeAssignment = model.DocumentTypeAssignment,
                PropertyAddress = model.PropertyAddress,
                FinalSalePrice = model.FinalSalePrice,
                FinalClosingDate = model.FinalClosingDate,
                DepositAmount = model.DepositAmount,
                DepositDate = model.DepositDate,
                BuyerName = model.BuyerName,
                LandLordName = model.LandLordName,
                ListingCommissionPercentage = model.ListingCommissionPercentage,
                BuyingCommissionPercentage = model.BuyingCommissionPercentage,
                ListingAgentName = model.ListingAgentName,
                ListingBrokerage = model.ListingBrokerage,
                ListingBrokerageFax = model.ListingBrokerageFax,
                BuyerAgentName = model.BuyerAgentName,
                BuyerBrokerage = model.BuyerBrokerage,
                BuyerBrokerageFax = model.BuyerBrokerageFax,
                SellersLawyer = model.SellersLawyer,
                SellersLawyerAddress = model.SellersLawyerAddress,
                SellersPhoneNumber = model.SellersPhoneNumber,
                BuyersLawyer = model.BuyersLawyer,
                BuyersLawyerAddress = model.BuyersLawyerAddress,
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
                DocumentTypeAssignmentId = viewModel.DocumentTypeAssignmentId,
                DocumentTypeAssignment = viewModel.DocumentTypeAssignment,
                PropertyAddress = viewModel.PropertyAddress,
                FinalSalePrice = viewModel.FinalSalePrice,
                FinalClosingDate = viewModel.FinalClosingDate,
                DepositAmount = viewModel.DepositAmount,
                DepositDate = viewModel.DepositDate,
                BuyerName = viewModel.BuyerName,
                LandLordName = viewModel.LandLordName,
                ListingCommissionPercentage = viewModel.ListingCommissionPercentage,
                BuyingCommissionPercentage = viewModel.BuyingCommissionPercentage,
                ListingAgentName = viewModel.ListingAgentName,
                ListingBrokerage = viewModel.ListingBrokerage,
                ListingBrokerageFax = viewModel.ListingBrokerageFax,
                BuyerAgentName = viewModel.BuyerAgentName,
                BuyerBrokerage = viewModel.BuyerBrokerage,
                BuyerBrokerageFax = viewModel.BuyerBrokerageFax,
                SellersLawyer = viewModel.SellersLawyer,
                SellersLawyerAddress = viewModel.SellersLawyerAddress,
                SellersPhoneNumber = viewModel.SellersPhoneNumber,
                BuyersLawyer = viewModel.BuyersLawyer,
                BuyersLawyerAddress = viewModel.BuyersLawyerAddress,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
            };
        }

        public static SaleListing ToSaleListingModel(this SaleListingViewModel viewModel)
        {
            return new SaleListing
            {
                TransactionTypeId = viewModel.TransactionTypeId,
                DocumentTypeAssignmentId = viewModel.DocumentTypeAssignmentId,
                DocumentTypeAssignment = viewModel.DocumentTypeAssignment,
                PropertyAddress = viewModel.PropertyAddress,
                FinalSalePrice = viewModel.FinalSalePrice,
                FinalClosingDate = viewModel.FinalClosingDate,
                DepositAmount = viewModel.DepositAmount,
                DepositDate = viewModel.DepositDate,
                BuyerName = viewModel.BuyerName,
                LandLordName = viewModel.LandLordName,
                ListingCommissionPercentage = viewModel.ListingCommissionPercentage,
                BuyingCommissionPercentage = viewModel.BuyingCommissionPercentage,
                ListingAgentName = viewModel.ListingAgentName,
                ListingBrokerage = viewModel.ListingBrokerage,
                ListingBrokerageFax = viewModel.ListingBrokerageFax,
                BuyerAgentName = viewModel.BuyerAgentName,
                BuyerBrokerage = viewModel.BuyerBrokerage,
                BuyerBrokerageFax = viewModel.BuyerBrokerageFax,
                SellersLawyer = viewModel.SellersLawyer,
                SellersLawyerAddress = viewModel.SellersLawyerAddress,
                SellersPhoneNumber = viewModel.SellersPhoneNumber,
                BuyersLawyer = viewModel.BuyersLawyer,
                BuyersLawyerAddress = viewModel.BuyersLawyerAddress,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt
            };
        }
        public static EditSaleListingViewModel ToEditSaleListingModel(this SaleListing model)
        {
            return new EditSaleListingViewModel
            {
                Id = model.Id,
                TransactionTypeId = model.TransactionTypeId,
                DocumentTypeAssignmentId = model.DocumentTypeAssignmentId,
                DocumentTypeAssignment = model.DocumentTypeAssignment,
                PropertyAddress = model.PropertyAddress,
                FinalSalePrice = model.FinalSalePrice,
                FinalClosingDate = model.FinalClosingDate,
                DepositAmount = model.DepositAmount,
                DepositDate = model.DepositDate,
                BuyerName = model.BuyerName,
                LandLordName = model.LandLordName,
                ListingCommissionPercentage = model.ListingCommissionPercentage,
                BuyingCommissionPercentage = model.BuyingCommissionPercentage,
                ListingAgentName = model.ListingAgentName,
                ListingBrokerage = model.ListingBrokerage,
                ListingBrokerageFax = model.ListingBrokerageFax,
                BuyerAgentName = model.BuyerAgentName,
                BuyerBrokerage = model.BuyerBrokerage,
                BuyerBrokerageFax = model.BuyerBrokerageFax,
                SellersLawyer = model.SellersLawyer,
                SellersLawyerAddress = model.SellersLawyerAddress,
                SellersPhoneNumber = model.SellersPhoneNumber,
                BuyersLawyer = model.BuyersLawyer,
                BuyersLawyerAddress = model.BuyersLawyerAddress,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}
