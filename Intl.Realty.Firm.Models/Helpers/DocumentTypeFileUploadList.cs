using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Helpers
{
    public class DocumentTypeFileUploadList
    {
        public int SaleListingId { get; set; }
        public List<DocumentTypeWithFileUpload>? DocumentTypeWithFileUploads { get; set; }
    }
}
