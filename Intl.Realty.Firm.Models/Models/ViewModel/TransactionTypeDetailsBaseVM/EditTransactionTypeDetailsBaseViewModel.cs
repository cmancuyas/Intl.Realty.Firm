using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.TransactionTypeDetailsBaseVM
{
    public class EditTransactionTypeDetailsBaseViewModel
    {
        public string PropertyAddress { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,4)")]
        public Decimal FinalSalePrice { get; set; }
        public DateTime FinalClosingDate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public Decimal DepositAmount { get; set; }
        public DateTime DepositDate { get; set; }
        public string BuyerName { get; set; } = string.Empty;
        public string LandLordName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,4)")]
        public Decimal ListingCommissionPercentage { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public Decimal BuyingCommissionPercentage { get; set; }
        public string ListingAgentName { get; set; } = string.Empty;
        public string ListingBrokerage { get; set; } = string.Empty;
        public string ListingBrokerageFax { get; set; } = string.Empty;
        public string BuyerAgentName { get; set; } = string.Empty;
        public string BuyerBrokerage { get; set; } = string.Empty;
        public string BuyerBrokerageFax { get; set; } = string.Empty;
        public string SellersLawyer { get; set; } = string.Empty;
        public string SellersLawyerAddress { get; set; } = string.Empty;
        public string SellersPhoneNumber { get; set; } = string.Empty;
        public string BuyersLawyer { get; set; } = string.Empty;
        public string BuyersLawyerAddress { get; set; } = string.Empty;
        public string BuyersPhoneNumber { get; set; } = string.Empty;
        [Required]
        public bool IsActive { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
