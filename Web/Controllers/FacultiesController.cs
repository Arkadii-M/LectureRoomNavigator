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

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddFaculty([FromBody] FacultyDTO faculty)
        {
            try
            {
                _facultyManager.AddFaculty(faculty);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public void Put([FromBody] FacultyDTO value)
        {
            _facultyManager.UpdateFacluty(value);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public void Delete([FromQuery] string id)
        {
            _facultyManager.RemoveFacultyById(id);
        }
    }
}
