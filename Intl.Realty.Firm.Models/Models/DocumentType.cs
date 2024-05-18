using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class DocumentType : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
