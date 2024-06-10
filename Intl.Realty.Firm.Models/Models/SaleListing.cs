using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class SaleListing : BaseModel
    {
        public int Id { get; set; }
        public string PropertyAddress { get; set; } = string.Empty;
        public int TransactionTypeId { get; set; }
        [JsonIgnore]
        public TransactionType? TransactionType { get; set; }
        public int IRFDealId { get; set; }  
    }
}
