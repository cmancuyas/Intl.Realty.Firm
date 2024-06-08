using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM 
{ 
    public class CreateSaleListingViewModel
    {
        [Key]
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType? TransactionType { get; set; }
        public int DocumentTypeAssignmentId { get; set; }
        public int DocumentTypeAssignment { get; set; }
        public string PropertyAddress { get; set; } = string.Empty;
        public decimal FinalSalePrice { get; set; }
        public DateTime FinalClosingDate { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime DepositDate { get; set; }
        public string BuyerName { get; set; } = string.Empty;
        public string LandLordName { get; set; } = string.Empty;
        public Decimal ListingCommissionPercentage { get; set; }
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
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
