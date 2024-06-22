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
    public class SaleListing : TransactionTypeDetailsBase
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        [JsonIgnore]
        public TransactionType? TransactionType { get; set; }
        [ForeignKey("IRFDeal")]
        public int IRFDealId { get; set; }
        public IRFDeal? IRFDeal { get; set; }
        [ForeignKey("FileUpload")]
        public int FileUploadId { get; set; }
        [JsonIgnore]
        public FileUpload? FileUpload { get; set; }
    }
}
