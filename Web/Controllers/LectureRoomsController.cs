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

        [HttpGet("{id}")]
        public DTO.Vertices.LectureRoomDTO Get(string id)
        {
            return _lectrueRoomManger.GetLectureRoomById(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Post([FromBody] LectureRoomDTO room)
        {
            room = _lectrueRoomManger.AddLectureRoom(room);
            return Created("", room);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult Put([FromBody] LectureRoomDTO value)
        {
            _lectrueRoomManger.UpdateLectureRoom(value);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IActionResult Delete([FromQuery] string id)
        {
            if (_lectrueRoomManger.RemoveLectureRoomById(id))
                return Ok();
            return BadRequest();
        }
    }
}
