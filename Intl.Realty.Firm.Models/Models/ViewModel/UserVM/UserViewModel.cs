using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.UserVM
{
    public class UserViewModel : BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? Suffix { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactNo { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        [Required]
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public EmploymentStatus? EmploymentStatus { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public int? ProfilePictureId { get; set; }
        public List<User>? Users { get; set; }
    }
}
