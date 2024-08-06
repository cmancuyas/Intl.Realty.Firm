using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.Models.Models.ViewModel.AccountVM
{
    public class AccountViewModel
    {
        public bool IsValidUsername { get; set; } = true;
        public bool IsValidPassword { get; set; } = true;

        [Required(ErrorMessage = "Email is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
        public string? ConfirmPassword { get; set; }
        public string SetUsername { get; set; } = "busybee_admin";
        public string SetPassword { get; set; } = "busybeeadmin1234";

        public MODE AccountMode { get; set; }

        public ResetPasswordViewModel? ResetPasswordViewModel { get; set; }

    }
    public enum MODE
    {
        SIGNIN,
        RESET,
        RESET_REQUEST
    }
}
