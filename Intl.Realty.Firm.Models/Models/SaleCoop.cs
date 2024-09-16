using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Intl.Realty.Firm.Models.Models
{
    public class SaleCoop : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        [JsonIgnore]
        public TransactionType? TransactionType { get; set; }
        public int IRFDealId { get; set; }
        public IRFDeal? IRFDeal { get; set; }
        public List<FileUpload>? FileUploads { get; set; }
        public int DealStatusId { get; set; }
        public DealStatus? DealStatus { get; set; }
    }
}
