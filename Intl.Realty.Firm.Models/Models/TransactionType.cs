﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models
{
    public class TransactionType : BaseModel
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
