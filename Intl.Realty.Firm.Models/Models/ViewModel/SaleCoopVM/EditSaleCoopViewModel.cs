using Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM;
using Intl.Realty.Firm.Models.Models.ViewModel.IRFDealVM;
using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.SaleCoopVM
{
    public class EditSaleCoopViewModel
    {
        [Key]
        public int Id { get; set; }
        public int TransactionTypeId {  get; set; } 
        public TransactionType? TransactionType { get; set; }
        public int IRFDealId { get; set; }
        public EditIRFDealViewModel? EditIRFDealViewModel { get; set; }
        public CreateFileUploadListViewModel? CreateFileUploadListViewModel { get; set; }
        public int DealStatusId { get; set; }
        public DealStatus? DealStatus { get; set; }
        public List<FileUpload>? FileUploads { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int UpdatedBy { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public IEnumerable<DocumentType>? DocumentTypeList { get; set; }
        public IEnumerable<DealStatus>? DealStatusList { get; set; }
        //used for selectize
        public List<string>? FileNames { get; set; }
    }
}
