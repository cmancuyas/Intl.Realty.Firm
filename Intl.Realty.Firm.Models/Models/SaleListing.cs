using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class SaleListing : TransactionTypeDetailsBase
    {
        public int Id { get; set; }
        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        [JsonIgnore]
        public TransactionType? TransactionType { get; set; }
        [ForeignKey("IRFDeal")]
        public int IRFDealId { get; set; }
        public IRFDeal? IRFDeal { get; set; }
    }
}
