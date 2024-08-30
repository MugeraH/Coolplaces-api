using Coolplaces.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Coolplaces.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<IdentityUser> userManager,IConfiguration config)
        {

            _userManager = userManager;
            _config = config;
        }

        public async Task<bool> RegisterUser(LoginUser user)
        {
            try {
                var identityUser = new IdentityUser { UserName = user.UserName, Email = user.UserName };

                var result = await _userManager.CreateAsync(identityUser, user.Password);
                return result.Succeeded;
                ;

            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                return false;
            }
        }


        public async Task<bool> Login(LoginUser user)
        {
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(user.UserName);
                if (identityUser is null)
                {
                    return false;
                };


                return await _userManager.CheckPasswordAsync(identityUser,user.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }



        public string GenerateTokenString(LoginUser user) {

            IEnumerable<System.Security.Claims.Claim> claims = new List<Claim> { 
            new Claim(ClaimTypes.Email,user.UserName),
             new Claim(ClaimTypes.Role,"Admin"),
            };


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            var signingCred = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
         
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials:signingCred
                
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        
        return tokenString;
        }
    }
}
