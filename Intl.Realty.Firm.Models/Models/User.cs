using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class User : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? Suffix { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string ContactNo { get; set; } = string.Empty;
        [Required]
        public int DepartmentId { get; set; }
        [Required]   
        public int RoleId { get; set; }
        [Required]
        public int EmploymentStatusId { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public DateTime? EmploymentDate { get; set; }
        public int? ProfilePictureId { get; set; }
        public Role? Role { get; set; }
        public EmploymentStatus? EmploymentStatus { get; set; }
        public Department? Department { get; set; }


    }
}
