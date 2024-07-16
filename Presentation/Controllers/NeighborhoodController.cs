using Entitiess.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class NeighborhoodController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public NeighborhoodController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("api/Neighborhood/GetAllNeighborhood")]
        public IActionResult GetAllNeighborhood()
        {
            var neighborhood = _manager.NeighborhoodService.GetAllNeighborhood(false);
            return Ok(neighborhood);
        }

        [HttpGet("api/Neighborhood/GetOneNeighborhoodById/{id:int}")]
        public IActionResult GetOneNeighborhoodById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var neighborhood = _manager
                .NeighborhoodService
                .GetOneNeighborhoodById(id, false);


                if (neighborhood is null)
                    return NotFound();

                return Ok(neighborhood);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("api/Neighborhood/GetNeighborhoodByLocalityId/{id:int}")]
        public IActionResult GetNeighborhoodByLocalityId([FromRoute(Name = "id")] int id)
        {
            try
            {
                var neighborhood = _manager
                .NeighborhoodService
                .GetNeighborhoodByLocalityId(id);

                if (neighborhood is null)
                    return NotFound();

                return Ok(neighborhood);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("api/Neighborhood/CreateOneNeighborhood")]
        public IActionResult CreateOneNeighborhood([FromBody] Entitiess.Models.Neighborhood neighborhood)
        {
            try
            {
                if (neighborhood is null)
                    return BadRequest();

                _manager.NeighborhoodService.CreateOneNeighborhood(neighborhood);


                return StatusCode(201, neighborhood);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/Neighborhood/DeleteOneNeighborhood/{id:int}")]
        public IActionResult DeleteOneNeighborhood([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.NeighborhoodService.DeleteOneNeighborhood(id, false);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
