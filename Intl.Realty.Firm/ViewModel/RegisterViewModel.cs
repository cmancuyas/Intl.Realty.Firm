using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace DENR_FAPIS.Models
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Suffix { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public string ContactNumber { get; set; } = string.Empty;

        public string OfficeName { get; set; } = string.Empty;
        public List<SelectListItem> OfficeNameList { get; set; } = new List<SelectListItem>();
        public string DivisionName { get; set; } = string.Empty;
        public List<SelectListItem> DivisionNameList { get; set; } = new List<SelectListItem>();
        public string Position { get; set; } = string.Empty;
        public List<SelectListItem> PositionList { get; set; } = new List<SelectListItem>();
        public string Designation {  get; set; } = string.Empty;
        public DateOnly DateOfEmployment { get; set; }
        public string EmploymentStatus { get; set; } = string.Empty;
        public List<SelectListItem> EmploymentStatusList { get; set; } = new List<SelectListItem>();
        public string SystemRole { get; set; } = string.Empty;
        public List<SelectListItem> SystemRoleList { get; set; } = new List<SelectListItem>();

        public string EmailAddress {  get; set; } = string.Empty;
        public string CreatePassword {  get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
