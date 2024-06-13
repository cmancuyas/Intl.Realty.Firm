using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.ProfilePictureVM
{
    public class ProfilePictureViewModel : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;
        // must be saved on this path wwwroot\Files\User-Files\Profile-Pictures\USER-{UserId}\
        public string WebDirectoryPath { get; set; } = string.Empty;
    }
}
