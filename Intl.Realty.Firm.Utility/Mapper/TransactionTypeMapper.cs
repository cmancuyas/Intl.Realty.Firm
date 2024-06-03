using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.TransactionTypeVM;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Name = model.Name,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static CreateTransactionTypeViewModel ToCreateTransactionTypeViewModel(this TransactionType model)
        {
            return new CreateTransactionTypeViewModel
            {
                Name = model.Name,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedAt = model.CreatedAt
            };
        }

        public static EditTransactionTypeViewModel ToEditTransactionTypeViewModel(this TransactionType model)
        {
            return new EditTransactionTypeViewModel
            {
                Id = model.Id,
                Name = model.Name,
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
                    Name = x.Name,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            }
            return viewModelList;
        }
    }

}
