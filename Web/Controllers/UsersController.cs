using BLL.Interface;
using DTO.Vertices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILogger _logger;
        public UsersController([FromServices] IUserManager userManager, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<UserDTO> Get()
        {
            var users = _userManager.GetAllUsers();
            return _userManager.GetAllUsers();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public UserDTO Get(string id)
        {
            var user = _userManager.GetUserById(id);
            return user;
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] UserDTO user)
        {
            user.Roles = new List<RoleDTO>() { new RoleDTO { Name = "user" } };
            this._userManager.AddUser(user);
        }

        // PUT api/<UsersController>/5
        [Authorize(Roles = "admin")]
        [HttpPut]
        public void Put([FromBody] UserDTO user)
        {
            _userManager.UpdateUser(user);
        }

        // PUT api/<UsersController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public void Delete([FromQuery] string id)
        {
            _userManager.RemoveUserById(id);
        }
    }
}
