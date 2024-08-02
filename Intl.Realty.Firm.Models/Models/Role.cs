using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    [Table("Roles")]
    public class Role : BaseModel
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
