using Microsoft.AspNetCore.Identity;
using Models.Enums;

namespace CreditApplication.Seeder
{
    public class RoleSeeder
    {
        public static async Task SeedRoleAsync(RoleManager<IdentityRole> roles)
        {
            foreach(var role in Enum.GetValues<Roles>())
            {
                if(!await roles.RoleExistsAsync(role.ToString()))
                {
                    await roles.CreateAsync(new IdentityRole(role.ToString()));
                }
            }
        }
    }
}
