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
    public class TransactionType : BaseModel
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //public int FileUploadId { get; set; }
        //public int MyProperty { get; set; }
        //[ForeignKey("SaleListing")]
        //public int SaleListingId { get; set; }
        //[JsonIgnore]
        //public SaleListing? SaleListing { get; set; }
        public int IRFDealId { get; set; }
        [JsonIgnore]
        public IRFDeal? IRFDeal { get; set; }
    }
}
