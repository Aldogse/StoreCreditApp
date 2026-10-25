using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Models.Users
{
    public class LoginCredentials: IdentityUser
    {
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
