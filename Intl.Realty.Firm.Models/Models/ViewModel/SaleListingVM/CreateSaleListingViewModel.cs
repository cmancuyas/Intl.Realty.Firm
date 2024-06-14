using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM 
{ 
    public class CreateSaleListingViewModel
    {
        public string PropertyAddress { get; set; } = string.Empty;
        public int TransactionTypeId { get; set; }
        public int IRFDealId { get; set; }
        public IRFDeal? IRFDeal { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public IEnumerable<SelectListItem>? TransactionTypeIEnum { get; set; }
        public List<DocumentTypeAssignment>? DocumentTypeAssignmentList { get; set; }
        public List<DocumentType>? DocumentTypeList { get; set; }
        public DocumentType? DocumentType { get; set; }
    }
}
