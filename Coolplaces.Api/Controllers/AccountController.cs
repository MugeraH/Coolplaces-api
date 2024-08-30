using Coolplaces.Api.Models;
using Coolplaces.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coolplaces.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(LoginUser user)
        {
            try {
                if(await _authService.RegisterUser(user))
                {
                    return Ok("Successfully done");
                }
                return BadRequest("Something went wrong");
            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser user)
        {

            try {

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (await _authService.Login(user))
                {

                    var tokenString =  _authService.GenerateTokenString(user);
                    return Ok(tokenString);
                };

                return BadRequest();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }


    }
}
