using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Helper
{
    public static class AuthHelper
    {
        public static bool IsNumeric(string value)
        {
            foreach (char c in value)
                if (!char.IsDigit(c))
                {
                    return false;
                }
            return true;

        }
        public static bool IsSpecialChar(string value)
        {


            foreach (var c in value)
            {
                if (ErrorMessages.Specialcharacters.Contains(c))
                {
                    return true;
                }
            }

            return false;
        }

        public static string GenerateJwtToken(string username, string role,IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, username),
              new Claim(ClaimTypes.Role, role)
               
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:ExpiresInMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        
    }
}
