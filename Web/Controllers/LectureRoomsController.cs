using BLL.Interface;
using DTO.Vertices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectureRoomsController : ControllerBase
    {
        private readonly ILectrueRoomManger _lectrueRoomManger;
        private readonly ILogger _logger;

        public LectureRoomsController([FromServices] ILectrueRoomManger lectrueRoomManger, ILogger<LectureRoomsController> logger)
        {
            _lectrueRoomManger = lectrueRoomManger;
            _logger = logger;
        }
        // GET: api/<LectureRoomsController>
        [HttpGet]
        public IEnumerable<DTO.Vertices.LectureRoomDTO> Get()
        {
            return _lectrueRoomManger.GetAllLectureRoom().AsEnumerable();
        }

        // GET api/<LectureRoomsController>/5
        [HttpGet("{id}")]
        public DTO.Vertices.LectureRoomDTO Get(string id)
        {
            return _lectrueRoomManger.GetLectureRoomById(id);
        }

        // POST api/<LectureRoomsController>
        [HttpPost]
        public void Post([FromBody] LectureRoomDTO value)
        {
            _lectrueRoomManger.AddLectureRoom(value);
        }

        // PUT api/<LectureRoomsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LectureRoomsController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _lectrueRoomManger.RemoveLectureRoomById(id);
        }
    }
}
