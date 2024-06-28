using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.FileCheckListVM
{
    public class CreateFileCheckListViewModel
    {
        public string? FileName { get; set; }
        public int? FileUploadId { get; set; }
        public int? DocumentTypeId { get; set; }
        public string? Status { get; set; }
        public List<string>? FileNames { get; set; }
    }
}
