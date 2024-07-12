using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM
{
    public class FileUploadViewModel : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;
        public string WebDirectoryPath { get; set; } = string.Empty;
        public string OriginalFileName { get; set; } = string.Empty;
        public int SaleListingId { get; set; }
        public int DocumentTypeId { get; set; }
        public int TransactionTypeId { get; set; }
        public SaleListing? SaleListing { get; set; }
        public DocumentType? DocumentType { get; set; }
        public TransactionType? TransactionType { get; set; }
    }
}
