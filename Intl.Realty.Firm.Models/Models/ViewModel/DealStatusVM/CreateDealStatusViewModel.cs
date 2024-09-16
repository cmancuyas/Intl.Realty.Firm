using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.DealStatusVM
{
    public class CreateDealStatusViewModel
    {
        [Required]
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
