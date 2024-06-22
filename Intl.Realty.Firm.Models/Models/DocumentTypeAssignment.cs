using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class DocumentTypeAssignment: BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentType? DocumentType { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType? TransactionType { get; set; }
    }
}
