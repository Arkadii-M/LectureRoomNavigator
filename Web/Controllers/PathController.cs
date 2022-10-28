using BLL.Interface;
using DTO.Path;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PathController : ControllerBase
    {
        private readonly IPathManager _pathManager;
        private readonly ILogger _logger;


        public PathController([FromServices] IPathManager pathManager, ILogger<PathController> logger)
        {
            _pathManager = pathManager;
            _logger = logger;
        }
        
        [HttpGet("{from_id}/{to_id}")]
        public IEnumerable<DTO.Vertices.Vertex> Get(string from_id,string to_id)
        {
            try
            {
                return _pathManager.GetOptimalPathBetweenVertices(from_id, to_id).VertexArray.AsEnumerable();
            }
            catch(Exception)
            {
                _logger.LogInformation("No path between vetrices from {From id} to {To id}",from_id,to_id);
                return Enumerable.Empty<DTO.Vertices.Vertex>();
            }
        }
    }
}
