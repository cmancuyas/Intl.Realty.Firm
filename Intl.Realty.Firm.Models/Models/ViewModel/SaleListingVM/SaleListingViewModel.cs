using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM
{
    public class SaleListingViewModel : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType? TransactionType { get; set; } = new TransactionType();
        public int IRFDealId { get; set; }
        public IRFDeal? IRFDeal { get; set; } = new IRFDeal();
        public ICollection<FileUpload>? FileUploads { get; set; }
    }
}
