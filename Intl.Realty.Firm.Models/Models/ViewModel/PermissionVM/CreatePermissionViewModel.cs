using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.PermissionVM
{
    public class CreatePermissionViewModel
    {
        [Required]
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ModuleId { get; set; }
        public Module? Module { get; set; }
        public int RolePermissionId { get; set; }
        public RolePermission? RolePermission { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
