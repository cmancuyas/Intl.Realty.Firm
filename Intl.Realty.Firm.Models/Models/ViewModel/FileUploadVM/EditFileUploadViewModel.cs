using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM
{
    public class EditFileUploadViewModel
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        public string Directory { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;
        public int SaleListingId { get; set; }
        public int DocumentTypeId { get; set; }
        public int TransactionTypeId { get; set; }

        public SaleListing? SaleListing { get; set; }
        public DocumentType? DocumentType { get; set; }
        public TransactionType? TransactionType { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int? UpdatedBy { get; set; }
        [Required]
        public DateTime? UpdatedAt { get; set; }
    }
}
