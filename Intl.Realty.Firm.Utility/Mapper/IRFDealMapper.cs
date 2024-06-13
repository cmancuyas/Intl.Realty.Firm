using Intl.Realty.Firm.Models.Models.ViewModel.IRFDealVM;
using Intl.Realty.Firm.Models.Models;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class IRFDealMapper
    {
        public static List<IRFDealViewModel> ToIRFDealListViewModel(this IEnumerable<IRFDeal> modelList)
        {
            List<IRFDealViewModel> viewModelList = new List<IRFDealViewModel>();

            if (modelList != null)
            {
                viewModelList = modelList.Select(o => new IRFDealViewModel
                {
                    Id = o.Id,
                    TransactionTypeId = o.TransactionTypeId,
                    DocumentTypeAssignmentList = o.DocumentTypeAssignmentList,
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
                    BuyersLawyerAddress = o.BuyersLawyerAddress,
                    IsActive = o.IsActive,
                    CreatedBy = o.CreatedBy,
                    CreatedAt = o.CreatedAt,
                    UpdatedBy = o.UpdatedBy,
                    UpdatedAt = o.UpdatedAt,
                }).ToList();
            }

            return viewModelList;
        }
        public static IRFDealViewModel ToIRFDealViewModel(this IRFDeal model)
        {
            return new IRFDealViewModel
            {
                Id = model.Id,
                TransactionTypeId = model.TransactionTypeId,
                DocumentTypeAssignmentList = model.DocumentTypeAssignmentList,
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
        public static IRFDeal ToIRFDealModel(this CreateIRFDealViewModel viewModel)
        {
            return new IRFDeal
            {
                TransactionTypeId = viewModel.TransactionTypeId,
                DocumentTypeAssignmentList = viewModel.DocumentTypeAssignmentList,
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

        public static IRFDeal ToIRFDealModel(this IRFDealViewModel viewModel)
        {
            return new IRFDeal
            {
                TransactionTypeId = viewModel.TransactionTypeId,
                DocumentTypeAssignmentList = viewModel.DocumentTypeAssignmentList,
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
        public static EditIRFDealViewModel ToEditIRFDealModel(this IRFDeal model)
        {
            return new EditIRFDealViewModel
            {
                Id = model.Id,
                TransactionTypeId = model.TransactionTypeId,
                DocumentTypeAssignmentList = model.DocumentTypeAssignmentList,
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
