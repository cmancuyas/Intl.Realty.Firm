using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DealStatusVM;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class DealStatusMapper
    {
        public static IEnumerable<DeleteDealStatusViewModel> ToDeleteDealStatusIEnumViewModel(this IEnumerable<DealStatus> modelIEnum)
        {
            IEnumerable<DeleteDealStatusViewModel> viewModelIEnum = new List<DeleteDealStatusViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DeleteDealStatusViewModel()
                {
                    Id = x.Id,
                });
            }
            return viewModelIEnum;
        }
        public static EditDealStatusViewModel ToEditDealStatusViewModel(this DealStatus model)
        {
            return new EditDealStatusViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description ?? "",
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static IEnumerable<DealStatusViewModel> ToDealStatusIEnumViewModel(this IEnumerable<DealStatus> modelIEnum)
        {
            IEnumerable<DealStatusViewModel> viewModelIEnum = new List<DealStatusViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DealStatusViewModel()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                });
            }
            return viewModelIEnum!;
        }
        public static DealStatus ToDealStatusModel(this CreateDealStatusViewModel viewModel)
        {
            var model = new DealStatus();
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.IsActive = viewModel.IsActive;
            model.CreatedAt = viewModel.CreatedAt;
            model.CreatedBy = viewModel.CreatedBy;
            return model;
        }
        public static DealStatus ToDealStatusModel(this EditDealStatusViewModel viewModel)
        {
            return new DealStatus
            {
                Id = viewModel.Id,
                Code = viewModel.Code,
                Description = viewModel.Description ?? "",
                IsActive = viewModel.IsActive,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt
            };
        }
    }
}