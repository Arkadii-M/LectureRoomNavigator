using BLL.Concrete;
using BLL.Interface;
using DTO.Edges;
using DTO.Vertices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationNodesController : ControllerBase
    {

        private readonly INavigationManager _navigationManager;
        private readonly ILogger _logger;


        public NavigationNodesController([FromServices] INavigationManager navigationManager, ILogger<PathController> logger)
        {
            _navigationManager = navigationManager;
            _logger = logger;
        }

        // GET: api/<NavigationNodesController>
        [HttpGet]
        public IEnumerable<NavigationNodeDTO> Get()
        {
            return _navigationManager.GetAllNavigationNodes().AsEnumerable();
        }

        // GET api/<NavigationNodesController>/5
        [HttpGet("{id}")]
        public NavigationNodeDTO Get(string id)
        {
            return _navigationManager.GetNavigationNodeById(id);
        }

        [Route("university_enter")]
        [HttpGet]
        public NavigationNodeDTO GetUniversityEnterNode()
        {
            return _navigationManager.GetEnterNode();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Post([FromBody] NavigationNodeDTO value)
        {
             var res = _navigationManager.AddNavigationNode(value);
            if (res.Id != String.Empty)
                return Ok();
            return BadRequest();
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult Put([FromBody] NavigationNodeDTO value)
        {
            _navigationManager.UpdateNavigationNode(value);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IActionResult Delete([FromQuery] string id)
        {
            if (_navigationManager.RemoveNavigationNodeById(id))
                return Ok();
            return BadRequest();
        }
    }
}
