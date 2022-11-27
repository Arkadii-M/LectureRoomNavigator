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
    public class NavigationEdgesController : ControllerBase
    {
        private readonly INavigationManager _navigationManager;
        private readonly ILogger _logger;

        public NavigationEdgesController([FromServices] INavigationManager navigationManager, ILogger<NavigationEdgesController> logger)
        {
            _navigationManager = navigationManager;
            _logger = logger;
        }

        // GET: api/<EdgesController>
        [HttpGet]
        public IEnumerable<NavigationEdgeDTO> GetAll()
        {
            var to_ret = _navigationManager.GetAllNavigationEdges(attach_elemnts: true);
            return to_ret;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddEdge([FromBody] NavigationEdgeDTO edge)
        {
            try
            {
                _navigationManager.AddNavigationEdge(edge);
            }
            catch(Exception exp)
            {
                _logger.LogError(exp.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}
