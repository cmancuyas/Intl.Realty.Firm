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
        public int TransactionTypeId { get; set; }
        public int DocumentTypeId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;
        // must be saved on this path wwwroot\Files\User-Files\Profile-Pictures\USER-{UserId}\
        public string WebDirectoryPath { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public List<string>? FileNames { get; set; }
    }
}
