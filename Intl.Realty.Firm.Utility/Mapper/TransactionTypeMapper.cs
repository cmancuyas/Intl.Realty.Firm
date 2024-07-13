using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.TransactionTypeVM;
using System.Reflection;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class TransactionTypeMapper
    {
        public static TransactionType FromCreateToTransactionTypeModel(this CreateTransactionTypeViewModel viewModel)
        {
            return new TransactionType
            {
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                CreatedAt = viewModel.CreatedAt,
                CreatedBy = viewModel.CreatedBy,
            };
        }
        public static TransactionType FromEditToTransactionTypeModel(this EditTransactionTypeViewModel viewModel)
        {
            return new TransactionType
            {
                Id = viewModel.Id,
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt,
            };
        }
        public static EditTransactionTypeViewModel ToEditTransactionTypeListViewModel(this TransactionType model)
        {
            return new EditTransactionTypeViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }

        public static List<TransactionType> FromModelToTransactionTypeListModel(this TransactionType model)
        {
            var modelList = new List<TransactionType>();
            if (model != null)
            {
                modelList.Add(model);
            }
            return modelList;
        }

        public static List<TransactionTypeViewModel> ToTransactionTypeListViewModel(this List<TransactionType> modelList)
        {
            var viewModelList = new List<TransactionTypeViewModel>();
            if (modelList != null)
            {
                viewModelList = modelList.Select(x => new TransactionTypeViewModel()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelList;
        }


        public static IEnumerable<TransactionTypeViewModel> FromIEnumToTransactionTypeIEnumViewModel(this IEnumerable<TransactionType> modelIEnum)
        {
            IEnumerable<TransactionTypeViewModel> viewModelIEnum = new List<TransactionTypeViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new TransactionTypeViewModel()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelIEnum;
        }
        
    }
}
