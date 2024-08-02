using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class User : BaseModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Suffix { get; set; }
        public DateTime BirthDate { get; set; }
        public string? ContactNo { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        [Required]   
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public EmployeeStatus EmployeeStatus { get; set; } = new EmployeeStatus();
        public DateTime? EmploymentDate { get; set; }
        public int? ProfilePictureId { get; set; }
        
        
    }
}
