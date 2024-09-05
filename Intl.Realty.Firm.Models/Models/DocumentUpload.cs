using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class DocumentUpload : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]  
        public int DocumentTypeId { get; set; }
        [Required]
        public int TransactionTypeId { get; set; }
        [Required]
        public int DocumentTypeAssignmentId { get; set; }
        public DocumentType? DocumentType { get; set; }
        public TransactionType? TransactionType { get; set; }
        public DocumentTypeAssignment? DocumentTypeAssignment { get; set; }
    }
}
