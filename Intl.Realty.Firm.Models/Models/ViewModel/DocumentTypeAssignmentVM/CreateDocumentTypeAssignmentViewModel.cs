using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM
{
    public class CreateDocumentTypeAssignmentViewModel
    {
        public int DocumentTypeId { get; set; }
        public int TransactionTypeId { get; set; }
        public List<DocumentType>? DocumentTypeList { get; set; }
        public List<TransactionType>? TransactionTypeList { get; set;}
        public IEnumerable<SelectListItem>? DocumentTypeIEnum { get; set; }
        public IEnumerable<SelectListItem>? TransactionTypeIEnum { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
