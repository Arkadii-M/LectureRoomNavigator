using BLL.Interface;
using DTO.Vertices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Helpers;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : Controller
    {

        private readonly IFacultyManager _facultyManager;
        private readonly ILogger _logger;

        public FacultiesController([FromServices] IFacultyManager facultyManager, ILogger<FacultiesController> logger)
        {
            _facultyManager = facultyManager;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<DTO.Vertices.FacultyDTO> Get()
        {
            return _facultyManager.GetAllFaculties();
        }

        [HttpGet("{id}")]
        public FacultyDTO Get(string id)
        {
            return _facultyManager.GetFacultyById(id);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddFaculty([FromBody] FacultyDTO faculty)
        {
            try
            {
                faculty = _facultyManager.AddFaculty(faculty);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Created("",faculty);
        }

        //[Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult Put([FromBody] FacultyDTO value)
        {
            _facultyManager.UpdateFacluty(value);
            return Ok();
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete]
        public IActionResult Delete([FromQuery] string id)
        {
            if (_facultyManager.RemoveFacultyById(id))
                return Ok();
            return BadRequest();
        }
    }
}
