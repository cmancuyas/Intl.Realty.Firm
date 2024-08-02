using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    [Table("RolePermissions")]
    public class RolePermission
    {
        public int Id { get; set; }

        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public Role? Roles { get; set; }
        public Permission? Permission { get; set; }
    }
}
