using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.IRFDealVM
{
    public class IRFDealViewModel : TransactionTypeDetailsBase
    {
        public int Id { get; set; }
        //[Required]
        //public string Code { get; set; } = string.Empty;
        //public string Description { get; set; } = string.Empty;
        public int FileUploadId { get; set; }
        [JsonIgnore]
        public FileUpload? FileUpload { get; set; }
        public int TransactionTypeId { get; set; }
        [JsonIgnore]
        public TransactionType? TransactionType { get; set; }
        [JsonIgnore]
        public List<DocumentTypeAssignment>? DocumentTypeAssignmentList { get; set; }
    }
}
