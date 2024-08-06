using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.RolePermissionVM
{
    public class EditRolePermissionViewModel
    {
        public int Id { get; set; }
        [Required]
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
