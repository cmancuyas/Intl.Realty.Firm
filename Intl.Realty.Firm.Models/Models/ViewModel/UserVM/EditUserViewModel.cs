using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.UserVM
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? Suffix { get; set; }
        public DateTime BirthDate { get; set; } = DateTime.UtcNow;
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
        public int EmploymentStatusId { get; set; }
        public EmploymentStatus? EmploymentStatus { get; set; }
        public DateTime? EmploymentDate { get; set; } = DateTime.UtcNow;
        public int? ProfilePictureId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<Department>? DepartmentList { get; set; }
        public List<Role>? RoleList { get; set; }
        public List<EmploymentStatus>? EmploymentStatusList { get; set; }
    }
}
