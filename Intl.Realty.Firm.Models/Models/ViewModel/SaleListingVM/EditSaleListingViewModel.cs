using Intl.Realty.Firm.Models.Helpers;
using Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM;
using Intl.Realty.Firm.Models.Models.ViewModel.IRFDealVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM
{
    public class EditSaleListingViewModel
    {
        [Key]
        public int Id { get; set; }
        public int TransactionTypeId {  get; set; } 
        public TransactionType? TransactionType { get; set; }
        public int IRFDealId { get; set; }
        public EditIRFDealViewModel? EditIRFDealViewModel { get; set; }
        public List<EditFileUploadViewModel>? EditFileUploadsViewModel { get; set; }
        public List<FileUpload>? FileUploads { get; set; }
        public FileUploadList? FileUploadList { get; set; }
        public string? Dataxxx { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int? UpdatedBy { get; set; }
        [Required]
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<DocumentType>? DocumentTypeList { get; set; }
        //used for selectize
        public List<string>? FileNames { get; set; }
    }
}
