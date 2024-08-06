using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.PermissionVM
{
    public class PermissionViewModel : BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ModuleId { get; set; }
        public Module? Module { get; set; }
        public int RolePermissionId { get; set; }
        public RolePermission? RolePermission { get; set; }
    }
}
