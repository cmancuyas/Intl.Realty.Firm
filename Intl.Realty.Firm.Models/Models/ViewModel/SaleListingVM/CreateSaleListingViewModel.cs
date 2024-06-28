using Intl.Realty.Firm.Models.Helpers;
using Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM;
using Intl.Realty.Firm.Models.Models.ViewModel.IRFDealVM;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM 
{ 
    public class CreateSaleListingViewModel
    {
        public int TransactionTypeId { get; set; }
        public TransactionType? TransactionType { get; set; }
        public int IRFDealId { get; set; }
        public CreateIRFDealViewModel CreateIRFDealViewModel { get; set; } = new CreateIRFDealViewModel();
        public ICollection<FileUpload>? FileUploads { get; set; }
        public List<CreateFileUploadViewModel>? CreateFileUploadsViewModel { get; set; }
        public FormFileUploadList? FileUploadList { get; set; }
        public string? Dataxxx { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public List<DocumentType>? DocumentTypeList { get; set; } = new List<DocumentType>();
    }
}
