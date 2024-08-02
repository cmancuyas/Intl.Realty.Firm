using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Intl.Realty.Firm.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string? FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; } = string.Empty;
        [Required]
        public string? LastName { get; set; } = string.Empty;
        public string? Suffix { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; } = DateTime.Now;
        [Required]
        public string? ContactNumber { get; set; } = string.Empty;
        [Required]
        public DateTime? DateOfEmployment { get; set; }
        [Required]
        public string? EmploymentStatus { get; set; } = string.Empty;
        public List<SelectListItem> EmploymentStatusList { get; set; } = new List<SelectListItem>();
        [Required]
        public string? SystemRole { get; set; } = string.Empty;
        public List<SelectListItem> SystemRoleList { get; set; } = new List<SelectListItem>();
        [Required]
        public string? EmailAddress { get; set; } = string.Empty;
        [Required]
        public string? CreatePassword { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(CreatePassword))]
        public string? ConfirmPassword { get; set; } = string.Empty;
        public IFormFile? ProfilePhoto { get; set; }
        public string? ReCaptchaSiteKey { get; set; }
    }
}
