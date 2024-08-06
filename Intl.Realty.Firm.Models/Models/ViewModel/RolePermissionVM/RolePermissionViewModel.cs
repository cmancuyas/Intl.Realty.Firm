using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.RolePermissionVM
{
    public class RolePermissionViewModel : BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
