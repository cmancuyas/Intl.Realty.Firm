using Intl.Realty.Firm.Models.Models.ViewModel.IRFDealVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM
{
    public class EditSaleListingViewModel
    {
        [Key]
        public int Id { get; set; }
        public string PropertyAddress { get; set; } = string.Empty;
        public TransactionType? TransactionType { get; set; }
        public int IRFDealId { get; set; }
        public EditIRFDealViewModel? EditIRFDealViewModel { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int? UpdatedBy { get; set; }
        [Required]
        public DateTime? UpdatedAt { get; set; }
    }
}
