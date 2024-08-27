using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.LeaseCoopVM
{
    public class LeaseCoopViewModel : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType? TransactionType { get; set; } = new TransactionType();
        public int IRFDealId { get; set; }
        public IRFDeal? IRFDeal { get; set; } = new IRFDeal();
        public List<FileUpload>? FileUploads { get; set; }
        public int DealStatusId { get; set; }
        public DealStatus? DealStatus { get; set; }
        public string? CreatedByFullName {  get; set; }
        public string? UpdatedByFullName {  get; set; }
    }
}
