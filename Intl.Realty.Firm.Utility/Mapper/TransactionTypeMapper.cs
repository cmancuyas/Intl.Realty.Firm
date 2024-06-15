using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.TransactionTypeVM;
using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class TransactionTypeMapper
    {
        public static TransactionTypeViewModel ToTransactionTypeViewModel(this TransactionType model)
        {
            return new TransactionTypeViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static TransactionType ToTransactionTypeModel(this CreateTransactionTypeViewModel viewModel)
        {
            return new TransactionType
            {
                Code = viewModel.Code,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
            };
        }
        public static CreateTransactionTypeViewModel ToCreateTransactionTypeViewModel(this TransactionType model)
        {
            return new CreateTransactionTypeViewModel
            {
                Code = model.Code,
                Description = model.Description,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt
            };
        }
        public static EditTransactionTypeViewModel ToEditTransactionTypeModel(this TransactionType model)
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
        public static EditTransactionTypeViewModel ToEditTransactionTypeViewModel(this TransactionType model)
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
        public static List<TransactionTypeViewModel> ToTransactionTypeListViewModel(this IEnumerable<TransactionType> modelList)
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
        public static List<TransactionType> FromIEnumToTransactionTypeList(this IEnumerable<TransactionType> modelIEnum)
        {
            var modelList = new List<TransactionType>();
            if (modelIEnum != null)
            {
                modelList = modelIEnum.Select(x => new TransactionType()
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
            return modelList;
        }
    }

}
