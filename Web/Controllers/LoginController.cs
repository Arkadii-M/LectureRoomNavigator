using BLL.Interface;
using DTO.Vertices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Helpers;

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
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.Role,"admin"), new Claim(ClaimTypes.Role, "user") };
            var jwt = new JwtSecurityToken(
                issuer: AuthHelper.Issuer,
                audience: AuthHelper.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    AuthHelper.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256
                    )
                );
            return Ok(new { username = user.UserName, token = new JwtSecurityTokenHandler().WriteToken(jwt) });
            //if (_userManager.LoginUser(user))
            //{
            //    return Ok(new { username =user.UserName,token = new JwtSecurityTokenHandler().WriteToken(jwt) });
            //}
            //return Forbid();
        }
    }
}
