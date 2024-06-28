using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class SaleListing : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        [JsonIgnore]
        public TransactionType? TransactionType { get; set; }
        public int IRFDealId { get; set; }
        public IRFDeal? IRFDeal { get; set; }
        public ICollection<FileUpload>? FileUploads { get; set; }
    }
}
