using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignment
{
    public class CreateDocumentTypeAssignmentViewModel : BaseModel
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public int TransactionTypeId { get; set; }
    }
}
