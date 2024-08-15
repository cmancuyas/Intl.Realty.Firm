using Intl.Realty.Firm.Models.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM
{
    public class CreateFileUploadViewModel
    {
        public string FileName { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        public string Directory { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;
        public int? DocumentTypeId { get; set; }
        public int? SaleListingId { get; set; }
        public int? TransactionTypeId { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        public List<IFormFile>? Files { get; set; }
        //used for selectize
        public List<string>? FileNames { get; set; }
    }
}
