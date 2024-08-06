using Intl.Realty.Firm.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.Models.ViewModel.AccountVM
{
    public class RegisterViewModel : BaseModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? Suffix { get; set; }
        [Required]
        public DateTime BirthDate { get; set; } = DateTime.Now;
        [Required]
        public string ContactNumber { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfEmployment { get; set; }

        [Required]
        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
        public int EmploymentStatusId { get; set; }
        public Role? Role { get; set; }
        public Department? Department { get; set; }
        public EmploymentStatus? EmploymentStatus { get; set; }

        [Required]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        public string CreatePassword { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(CreatePassword))]
        public string ConfirmPassword { get; set; } = string.Empty;
        public IFormFile? ProfilePhoto { get; set; }
        public int? ProfilePictureId { get; set; }
        public string ReCaptchaSiteKey { get; set; } = string.Empty;

        public IEnumerable<SelectListItem>? DepartmentIEnum { get; set; }
        public IEnumerable<SelectListItem>? EmploymentStatusIEnum { get; set; }
        public IEnumerable<SelectListItem>? RoleIEnum { get; set; }
    }
}
