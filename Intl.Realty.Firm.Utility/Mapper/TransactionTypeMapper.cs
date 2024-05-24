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
        public static TransactionTypeViewModel ToTransactionTypeViewModel(this TransactionType transactionTypeModel)
        {
            return new TransactionTypeViewModel
            {
                Id = transactionTypeModel.Id,
                Name = transactionTypeModel.Name,
                IsActive = transactionTypeModel.IsActive,
                CreatedBy = transactionTypeModel.CreatedBy,
                CreatedAt = transactionTypeModel.CreatedAt,
                UpdatedBy = transactionTypeModel.UpdatedBy,
                UpdatedAt = transactionTypeModel.UpdatedAt
            };
        }
        public static CreateTransactionTypeViewModel ToCreateTransactionTypeViewModel(this TransactionType transactionTypeModel)
        {
            return new CreateTransactionTypeViewModel
            {
                Name = transactionTypeModel.Name,
                IsActive = transactionTypeModel.IsActive,
                CreatedBy = transactionTypeModel.CreatedBy,
                CreatedAt = transactionTypeModel.CreatedAt,
                UpdatedBy = transactionTypeModel.UpdatedBy,
                UpdatedAt = transactionTypeModel.UpdatedAt
            };
        }

        public static TransactionType ToTransactionTypeModel(this TransactionTypeViewModel transactionTypeViewModel)
        {
            return new TransactionType
            {
                Name = transactionTypeViewModel.Name,
                IsActive = transactionTypeViewModel.IsActive,
                CreatedBy = transactionTypeViewModel.CreatedBy,
                CreatedAt = transactionTypeViewModel.CreatedAt,
                UpdatedBy = transactionTypeViewModel.UpdatedBy,
                UpdatedAt = transactionTypeViewModel.UpdatedAt
            };
        }
    }

}
