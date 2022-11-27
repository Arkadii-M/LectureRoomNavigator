using BLL.Interface;
using DTO.Vertices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
            if (_userManager.LoginUser(user))
            {
                var roles = _userManager.GetUserRoles(user.UserName);

                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName)};

                foreach(var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }

                var jwt = new JwtSecurityToken(
                    issuer: AuthHelper.Issuer,
                    audience: AuthHelper.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(AuthHelper.TokenLifetime),
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                        AuthHelper.GetSymmetricSecurityKey(),
                        SecurityAlgorithms.HmacSha256
                        )
                    );
                return Ok(new { username = user.UserName, token = new JwtSecurityTokenHandler().WriteToken(jwt) });
            }
            return Forbid("No user found");
        }
    }
}
