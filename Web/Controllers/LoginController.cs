using BLL.Interface;
using DTO.Vertices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILogger _logger;

        public LoginController([FromServices] IUserManager userManager, ILogger<LoginController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserDTO user)// TODO add Cookie
        {
            if(_userManager.LoginUser(user))
            {
                return Ok();
            }
            return Forbid();
        }
    }
}
