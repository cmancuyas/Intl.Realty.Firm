﻿using Intl.Realty.Firm.Models.Models;
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
                CreatedAt = model.CreatedAt,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }

        public static TransactionType ToTransactionTypeModel(this TransactionTypeViewModel viewModel)
        {
            return new TransactionType
            {
                Name = viewModel.Name,
                IsActive = viewModel.IsActive,
                CreatedBy = viewModel.CreatedBy,
                CreatedAt = viewModel.CreatedAt,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt
            };
        }
    }

}
