using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.ProfilePictureVM
{
    public class ProfilePictureViewModel : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        public string Directory { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;
    }
}
