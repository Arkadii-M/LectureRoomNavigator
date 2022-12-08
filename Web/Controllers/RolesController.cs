using BLL.Concrete;
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
        public IActionResult Post([FromBody] RoleDTO role)
        {
            role = _roleManger.AddRole(role);
            return Created("", role);
        }

        // PUT api/<UsersController>/5
        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult Put([FromBody] RoleDTO role)
        {
            _roleManger.UpdateRole(role);
            return Ok();
        }
        // PUT api/<UsersController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IActionResult Delete([FromQuery] string id)
        {
            if (_roleManger.DeleteRole(id))
                return Ok();
            return BadRequest();
        }
    }
}
