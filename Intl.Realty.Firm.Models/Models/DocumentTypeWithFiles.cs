using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class DocumentTypeWithFiles
    {
        public int Id { get; set; }
        public int? DocumentTypeId { get; set; }
        public List<IFormFile>? Files { get; set; }

    }
}
