using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.CustomerVM
{
    public class EditCustomerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        [Required]
        public bool IsActive { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
