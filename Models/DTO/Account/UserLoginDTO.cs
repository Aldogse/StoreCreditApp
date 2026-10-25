using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Account
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Username is required!")]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(50)]
        public string Password { get; set; } = string.Empty;
    }
}
