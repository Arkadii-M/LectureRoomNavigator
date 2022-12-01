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
    public class RolesController : ControllerBase
    {
        private readonly IRoleManager _roleManger;
        private readonly ILogger _logger;
        public RolesController([FromServices] IRoleManager roleManger, ILogger<RolesController> logger)
        {
            _roleManger = roleManger;
            _logger = logger;
        }

        // GET: api/<RolesController>
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IEnumerable<RoleDTO> Get()
        {
            return _roleManger.GetAllRoles();
        }

        // POST api/<RolesController>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public void Post([FromBody] RoleDTO role)
        {
            _roleManger.AddRole(role);
        }

        // PUT api/<UsersController>/5
        [Authorize(Roles = "admin")]
        [HttpPut]
        public void Put([FromBody] RoleDTO role)
        {
            _roleManger.UpdateRole(role);
        }
        // PUT api/<UsersController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public void Delete([FromQuery] string id)
        {
            _roleManger.DeleteRole(id);
        }
    }
}
