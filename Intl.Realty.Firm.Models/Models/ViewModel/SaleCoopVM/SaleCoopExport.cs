namespace Intl.Realty.Firm.Models.Models.ViewModel.SaleCoopVM 
{ 
    public class SaleCoopExport
    {
        public string TransactionType { get; set; } = string.Empty;
        public string PropertyAddress { get; set; } = string.Empty;
        public string FinalSalePrice { get; set; } = string.Empty;
        public DateTime FinalClosingDate { get; set; } = DateTime.UtcNow;
        public string DepositAmount = string.Empty;
        public DateTime DepositDate { get; set; } = DateTime.UtcNow;
        public string BuyerName { get; set; } = string.Empty;
        public string LandLordName { get; set; } = string.Empty;
        public string ListingCommissionPercentage { get; set; } = string.Empty;
        public string BuyingCommissionPercentage { get; set; } = string.Empty;
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
        public string DealStatus { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
   
    }
}
