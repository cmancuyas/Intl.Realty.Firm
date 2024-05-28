﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM
{
    public class EditDocumentTypeAssignmentViewModel : BaseModel
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public int TransactionTypeId { get; set; }
        public List<DocumentType>? DocumentTypeList { get; set; }
        public List<TransactionType>? TransactionTypeList { get; set;}
    }
}
