using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.UserProfileVM
{
    public class ViewUserProfileViewModel
    {
        public int ProfilePictureId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactNo { get; set; }

        public DateTime EmploymentDate { get; set; }
        public EmploymentStatus EmployeeStatus { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
    }
}
