using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Account
{
    public class AccountRegistrationDTO
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; } = string.Empty;

        [StringLength(50)]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; } = string.Empty;

        [StringLength(50)]
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password",ErrorMessage = "Password is not matched!")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
