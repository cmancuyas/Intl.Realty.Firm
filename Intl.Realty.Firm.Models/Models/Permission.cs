using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class Permission : BaseModel
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int ModuleId { get; set; }
        public Module? Module { get; set; }
        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
