using ECommerce.Models.DTO;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Repository.IRepository;
using ECommerce.Helper;
using ECommerce.Repository;
using Microsoft.Extensions.Configuration;


namespace ECommerce.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        [HttpPost("User")]
       public async Task<IActionResult> Signin(UserLoginDTO user)
        {
        if (AuthHelper.IsNumeric(user.Username) || AuthHelper.IsSpecialChar(user.Username))
        {
            return Unauthorized(ErrorMessages.InvalidUsername);
        }

        var existingUser = await _userRepository.GetUserByUsernameAndPassword(user.Username, user.Password);
        if (existingUser != null)
        {
            var token = AuthHelper.GenerateJwtToken(existingUser.Username, existingUser.Role,_configuration);
            return Ok(new { token });
        }

        return Unauthorized("Invalid Login");
    }
        [HttpPost("SignUp")]
        public async Task<IActionResult> Signup(UserSignupDTO user)
        {
            if (user.Username == ErrorMessages.UserName)
            {
                return Unauthorized(ErrorMessages.Exists);
            }
            if (AuthHelper.IsNumeric(user.Username) || AuthHelper.IsSpecialChar(user.Username))
            {
                return Unauthorized(ErrorMessages.InvalidUsername);
            }

            if (await _userRepository.UserExists(user.Username))
            {
                return BadRequest(ErrorMessages.Exists);
            }

            PasswordHelper.CreatePasswordHash(user.Password, out string passwordHash, out string passwordSalt);

            var newUser = new User
            {
                Username = user.Username,
                PasswordHash = passwordHash,
                Passwordsalt = passwordSalt,
                Role = "User"
            };

            await _userRepository.AddUser(newUser);
            return Ok("Registered Successfully");
        }






    }
}
