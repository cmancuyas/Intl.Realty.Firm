using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.TransactionTypeVM
{
    public class TransactionTypeExport
    {
        public string Code { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
    }
}
