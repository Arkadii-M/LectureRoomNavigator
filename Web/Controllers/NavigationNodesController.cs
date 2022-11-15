﻿using BLL.Interface;
using DTO.Edges;
using DTO.Vertices;
using Microsoft.AspNetCore.Mvc;

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

        //[HttpGet("byIds")]
        //public IEnumerable<NavigationNodeDTO> GetByIds(string[] nav_ids)
        //{
        //    return _navigationManager.GetNavigationNodesByIds(nav_ids).AsEnumerable();
        //}

        // GET api/<NavigationNodesController>/5
        [HttpGet("{id}")]
        public NavigationNodeDTO Get(string id)
        {
            return _navigationManager.GetNavigationNodeById(id);
        }

        // POST api/<NavigationNodesController>
        [HttpPost]
        public IActionResult Post([FromBody] NavigationNodeDTO value)
        {
             var res = _navigationManager.AddNavigationNode(value);
            if (res.Id != String.Empty)
                return Ok();
            return BadRequest();
        }

        // PUT api/<NavigationNodesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NavigationNodesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}