using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ECommerce.Models;
using ECommerce.Models.DTO;
using ECommerce.Repository.IRepository;
using ECommerce.Helper;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {
       
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            
            _configuration = configuration;
        }

        [HttpPost("Admin")]
        public async Task<IActionResult> Signin(UserLoginDTO user)
        {
            try 
            { 

            if (AuthHelper.IsNumeric(user.Username) || AuthHelper.IsSpecialChar(user.Username))
            {
                return Unauthorized(ErrorMessages.InvalidUsername);
            }


            //var existingUser = await _adminRepository.GetUserByUsernameAndPassword(user.Username,user.Password);


            if (user.Username == ErrorMessages.UserName && user.Password == ErrorMessages.PassWord)
            {
                var token = AuthHelper.GenerateJwtToken(user.Username, "Admin",_configuration);
                return Ok(new { token });

            }



            return Unauthorized("Invalid Login"); }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
