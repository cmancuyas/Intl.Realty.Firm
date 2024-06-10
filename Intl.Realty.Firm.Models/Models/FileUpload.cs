namespace Intl.Realty.Firm.Models.Models
{
    public class FileUpload : BaseModel
    {
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public int IRFDealId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileSize {  get; set; } = string.Empty;
        
    }
}
