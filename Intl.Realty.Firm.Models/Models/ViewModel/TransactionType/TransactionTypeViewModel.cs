﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.TransactionType
{
    public class TransactionTypeViewModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
