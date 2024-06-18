using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intl.Realty.Firm.Models.Models
{
    public class FileUpload : BaseModel
    {
        public int Id { get; set; }
        [ForeignKey("IRFDeal")]
        public int IRFDealId { get; set; }
        [ForeignKey("DocumentType")]
        public int DocumentTypeId { get; set; }
        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileSize {  get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string WebDirectoryPath { get; set; } = string.Empty;
        [JsonIgnore]
        public IRFDeal? IRFDeal { get; set; }
        [JsonIgnore]
        public DocumentType? DocumentType { get; set; }
        [JsonIgnore]
        public TransactionType? TransactionType { get; set; }
    }
}
