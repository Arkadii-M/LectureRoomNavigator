using BLL.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LectureRoomNavigator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PathController : ControllerBase
    {
        private readonly IPathManager _pathManager;
        public PathController([FromServices] IPathManager pathManager)
        {
            _pathManager = pathManager;
        }
        // GET: api/<NavigationController>
        [HttpGet("{from_id}/{to_id}")]
        public IEnumerable<DTO.Path.SimplePathDTO> GetAll(string from_id, string to_id)
        {
            return _pathManager.GetAllPathesBetweenVertices(from_id, to_id).AsEnumerable();
        }

        [HttpGet("optimal/{from_id}/{to_id}")]
        public DTO.Path.SimplePathDTO GetOptimal(string from_id, string to_id)
        {
            return _pathManager.GetOptimalPathBetweenVertices(from_id, to_id);
        }

        // GET api/<NavigationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NavigationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NavigationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NavigationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
