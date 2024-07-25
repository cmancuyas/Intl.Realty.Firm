using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Helpers
{
    public class FileUploadList : Response
    {
        public List<IFormFile>? Files { get; set; }
    }
}
