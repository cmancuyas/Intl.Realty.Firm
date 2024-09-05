using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models
{
    public class BaseModel
    {
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
