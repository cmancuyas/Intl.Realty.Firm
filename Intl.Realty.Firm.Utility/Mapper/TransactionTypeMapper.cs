using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.TransactionTypeVM;
using Intl.Realty.Firm.Models.Models.ViewModel.TransactionTypeVM;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class TransactionTypeMapper
    {
        public static IEnumerable<DeleteTransactionTypeViewModel> ToDeleteTransactionTypeIEnumViewModel(this IEnumerable<TransactionType> modelIEnum)
        {
            IEnumerable<DeleteTransactionTypeViewModel> viewModelIEnum = new List<DeleteTransactionTypeViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DeleteTransactionTypeViewModel()
                {
                    Id = x.Id,
                });
            }
            return viewModelIEnum;
        }
        public static IEnumerable<TransactionTypeViewModel> ToTransactionTypeIEnumViewModel(this IEnumerable<TransactionType> modelIEnum)
        {
            IEnumerable<TransactionTypeViewModel> viewModelIEnum = new List<TransactionTypeViewModel>();
            if (viewModelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new TransactionTypeViewModel()
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
        public static EditTransactionTypeViewModel ToEditTransactionTypeViewModel(this TransactionType model)
        {
            return new EditTransactionTypeViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description ?? "",
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static TransactionType ToTransactionTypeModel(this CreateTransactionTypeViewModel viewModel)
        {
            var model = new TransactionType();
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.IsActive = viewModel.IsActive;
            model.CreatedAt = viewModel.CreatedAt;
            model.CreatedBy = viewModel.CreatedBy;
            return model;
        }
        public static TransactionType ToTransactionTypeModel(this EditTransactionTypeViewModel viewModel)
        {
            return new TransactionType
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
