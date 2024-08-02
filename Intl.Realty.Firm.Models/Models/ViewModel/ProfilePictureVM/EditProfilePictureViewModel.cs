using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.ProfilePictureVM
{
    public class EditProfilePictureViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        public string Directory { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int? UpdatedBy { get; set; }
        [Required]
        public DateTime? UpdatedAt { get; set; }
    }
}
