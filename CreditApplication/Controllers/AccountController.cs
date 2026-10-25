using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.Users;
using Models.Enums;
using CreditApplication.Services;
using Models.DTO.Account;

namespace CreditApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<LoginCredentials> _signInManager;
        private readonly UserManager<LoginCredentials> _userManager;
        private readonly JwtService _jwtService;


        public AccountController(SignInManager<LoginCredentials> signInManager,
            UserManager<LoginCredentials> userManager,
            JwtService jwtService
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        //OWNER ACCOUNT REGISRATION PROCESS
        [HttpPost("Registration")]
        public async Task<IActionResult> StoreOwnerRegistration([FromBody]AccountRegistrationDTO accountRegistrationDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exist = await _userManager.FindByEmailAsync(accountRegistrationDTO.Email);

            if (exist != null)
            {
                return StatusCode(400, new
                {
                    message = "Email already exist!"
                });
            }

            var newUser = new LoginCredentials
            {
                Email = accountRegistrationDTO.Email,
                UserName = accountRegistrationDTO.Username,
            };

            //create the user 
            var newUserCreds = await _userManager.CreateAsync(newUser,accountRegistrationDTO.Password);

            if (newUserCreds.Succeeded)
            {
                //add role to the user 
                //THIS IS OWNER REGISTRATION ENDPOINT
                var role = await _userManager.AddToRoleAsync(newUser,Roles.Owner.ToString());

                return StatusCode(200, new
                {
                    message = "Owner account successfully created!"
                });
            }

            return BadRequest(ModelState);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, new
                {
                    message = $"Error: {ModelState}"
                });
            }

            var user = await _userManager.FindByNameAsync(userLoginDTO.Username);

            if(user == null)
            {
                return StatusCode(400,new
                {
                    message = "Incorrect Username or Password"
                });
            }

            var loginUser = await _signInManager.PasswordSignInAsync(userLoginDTO.Username,userLoginDTO.Password,false,false);

            if (loginUser.Succeeded)
            {
               var token = await _jwtService.GenerateTokenAsync(user);

                return Ok(new
                {
                    message = "Log in success!",
                    accessToken = token,
                    TokenType = "Bearer"
                });
            }

            return BadRequest(new
            {
                message = "Incorrect Username or Password!"
            });
        }
    }
}
