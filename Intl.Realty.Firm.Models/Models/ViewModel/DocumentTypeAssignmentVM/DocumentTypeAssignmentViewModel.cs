using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM
{
    public class DocumentTypeAssignmentViewModel : BaseModel
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentType? DocumentType { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType? TransactionType { get; set; }
    }
}
