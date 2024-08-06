using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.AccountVM
{
    public class ResetPasswordViewModel : ResetResponse
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be greater than 7 characters")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{0,}$",
            ErrorMessage = "Password must contain at least one uppercase, one lowercase, one digit and one special character")]
        public string? Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required")]
        [MinLength(8, ErrorMessage = "Confirm Password must be greater than 7 characters")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{0,}$",
            ErrorMessage = "Confirm Password must contain at least one uppercase, one lowercase, one digit and one special character")]
        [Compare(nameof(Password), ErrorMessage = "Password did not match")]
        public string? ConfirmPassword { get; set; } = string.Empty;
    }

    public class ResetResponse
    {
        public bool IsSuccess { get; set; } = false;
        public int RequestToken { get; set; }
    }
}
