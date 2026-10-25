using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using Models.Users;

namespace CreditApplication.Services
{
    public class JwtService
    {
        private readonly UserManager<LoginCredentials> _userManager;
        private readonly IConfiguration _configuration;

        public JwtService(UserManager<LoginCredentials> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task <string> GenerateTokenAsync(LoginCredentials user)
        {
            //CLAIM WILL BE USING IS ID AND ROLE
            List<Claim> claims = new List<Claim>
            {
                //USERNAME OF THE USER
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Token"] ?? ""));
            var signingCreds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken
                (
                   issuer: _configuration["JWT:Issuer"],
                   claims: claims,
                   expires: DateTime.Now.AddHours(1),
                   signingCredentials: signingCreds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
