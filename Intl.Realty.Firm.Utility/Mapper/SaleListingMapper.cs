using Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM;
using Intl.Realty.Firm.Models.Models;
using System.Reflection;

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
                    TransactionType = o.TransactionType,
                    IRFDealId = o.IRFDealId,
                    IRFDeal = o.IRFDeal,
                    FileUploadId = o.FileUploadId, 
                    FileUpload = o.FileUpload,
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
                TransactionTypeId = model.TransactionTypeId,
                TransactionType = model.TransactionType,
                IRFDealId = model.IRFDealId,
                IRFDeal = model.IRFDeal,
                FileUploadId = model.FileUploadId,
                FileUpload = model.FileUpload,
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
                FileUploadId = viewModel.FileUploadId,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
            };
        }

        public static SaleListing ToSaleListingModel(this SaleListingViewModel viewModel)
        {
            return new SaleListing
            {
                Id = viewModel.Id,
                TransactionTypeId = viewModel.TransactionTypeId,
                IRFDealId = viewModel.IRFDealId,
                FileUploadId = viewModel.FileUploadId,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt,
            };
        }
        public static EditSaleListingViewModel ToEditSaleListingModel(this SaleListing model)
        {
            return new EditSaleListingViewModel
            {
                Id = model.Id,
                TransactionTypeId = model.TransactionTypeId,
                TransactionType = model.TransactionType,
                IRFDealId = model.IRFDealId,
                EditIRFDealViewModel = model.IRFDeal?.ToEditIRFDealModel(),
                FileUploadId = model.FileUploadId,
                EditFileUploadViewModel = model.FileUpload?.ToEditFileUploadModel(),
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }


    }
}
