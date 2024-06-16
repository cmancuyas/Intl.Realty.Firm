using Intl.Realty.Firm.Models.Models.ViewModel.IRFDealVM;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM 
{ 
    public class CreateSaleListingViewModel
    {
        public string PropertyAddress { get; set; } = string.Empty;
        public int TransactionTypeId { get; set; }
        public int IRFDealId { get; set; }
        public CreateIRFDealViewModel CreateIRFDealViewModel { get; set; } = new CreateIRFDealViewModel();
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public IEnumerable<SelectListItem>? TransactionTypeIEnum { get; set; }
        public List<DocumentTypeAssignment>? DocumentTypeAssignmentList { get; set; }
        public List<DocumentType>? DocumentTypeList { get; set; } = new List<DocumentType>();
        public DocumentType? DocumentType { get; set; }
    }
}
