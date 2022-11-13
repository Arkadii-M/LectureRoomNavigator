using BLL.Interface;
using DTO.Edges;
using DTO.Vertices;
using Microsoft.AspNetCore.Mvc;

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

        //[HttpGet("byIds")]
        //public IEnumerable<NavigationEdgeDTO> GetByIds([FromBody] List<string> nav_ids)
        //{
        //    return _navigationManager.GetNavigationEdgesByIds(nav_ids.ToArray()).AsEnumerable();
        //}


        // GET: api/<EdgesController>
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

        //[HttpGet]
        //public IEnumerable<Tuple<Tuple<float,float>, Tuple<float, float>>> GetNavCoordinates()
        //{
        //    return _navigationManager.GetAllNavigationEdges();
        //}

        //// GET api/<EdgesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<EdgesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<EdgesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EdgesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
