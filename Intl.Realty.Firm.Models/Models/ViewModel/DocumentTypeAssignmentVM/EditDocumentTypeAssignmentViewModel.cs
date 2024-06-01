using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM
{
    public class EditDocumentTypeAssignmentViewModel
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public int TransactionTypeId { get; set; }
        public List<DocumentType>? DocumentTypeList { get; set; }
        public List<TransactionType>? TransactionTypeList { get; set;}
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int? UpdatedBy { get; set; }
        [Required]
        public DateTime? UpdatedAt { get; set; }
    }
}
