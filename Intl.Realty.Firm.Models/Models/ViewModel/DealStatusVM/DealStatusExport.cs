using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.DealStatusVM
{
    public class DealStatusExport
    {
        [Required]
        public string Code { get; set; } = string.Empty;
        public string DealStatus { get; set; } = string.Empty;
    }
}
