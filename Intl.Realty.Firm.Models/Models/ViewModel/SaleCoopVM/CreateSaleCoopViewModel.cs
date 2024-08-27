using Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM;
using Intl.Realty.Firm.Models.Models.ViewModel.IRFDealVM;
using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.SaleCoopVM 
{ 
    public class CreateSaleCoopViewModel
    {
        public int TransactionTypeId { get; set; }
        public TransactionType? TransactionType { get; set; }
        public int IRFDealId { get; set; }
        public CreateIRFDealViewModel CreateIRFDealViewModel { get; set; } = new CreateIRFDealViewModel();
        public CreateFileUploadListViewModel? CreateFileUploadListViewModel { get; set; }
        public int DealStatusId { get; set; }
        public DealStatus? DealStatus { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public IEnumerable<DocumentType>? DocumentTypeList { get; set; }
    }
}
