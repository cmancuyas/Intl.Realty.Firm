using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM
{
    public class FormFileIEnumWithDocType
    {
        public IEnumerable<IFormFile>? FormFileIEnum;
        public int DocumentTypeId;
    }
}
