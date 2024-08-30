using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coolplaces.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JointController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public string Get()
        {
            return "You got to me!";
        }

    }
}
