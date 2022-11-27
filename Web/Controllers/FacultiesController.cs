using BLL.Concrete;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }
}
