using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class SaleListing : BaseModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
