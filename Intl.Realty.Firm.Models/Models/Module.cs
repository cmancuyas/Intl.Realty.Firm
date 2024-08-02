using Intl.Realty.Firm.Models.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    [Table("Modules")]
    public class Module
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Permission>? Permission { get; set; }
        public ActivityLog? Log { get; set; }
    }
}
