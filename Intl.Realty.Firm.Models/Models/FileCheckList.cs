using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class FileCheckList : BaseModel
    {
        public int Id { get; set; }
        public int? FileUploadId { get; set; }
        public int? DocumentTypeId { get; set; }
        public string? Status { get; set; }

    }
}
