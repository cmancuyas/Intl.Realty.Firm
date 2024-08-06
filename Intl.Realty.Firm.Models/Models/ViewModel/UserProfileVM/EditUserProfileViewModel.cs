using Intl.Realty.Firm.Models.Models.ViewModel.ProfilePictureVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.UserProfileVM
{
    public class EditUserProfileViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? Suffix { get; set; }
        public DateTime BirthDate { get; set; }
        public string? ContactNo { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public EditProfilePictureViewModel? EditProfilePictureViewModel { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public EmploymentStatus? EmployeeStatus { get; set; }
        public DateTime? EmploymentDate { get; set; }

        //Allow Edit
        public bool allowEdit { get; set; }

    }
}
