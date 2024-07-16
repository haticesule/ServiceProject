using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class CityController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public CityController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet("api/City/GetAllCity")]
        public IActionResult GetAllCity()
        {
            var city = _manager.CityService.GetAllCity(false);
            return Ok(city);
        }

        [HttpGet("api/City/GetOneCityById/{id:int}")]
        public IActionResult GetOneCityById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var city = _manager
                .CityService
                .GetOneCityById(id, false);


                if (city is null)
                    return NotFound();

                return Ok(city);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("api/City/CreateOneCity")]
        public IActionResult Cra([FromBody] Entitiess.Models.City city)
        {
            try
            {
                if (city is null)
                    return BadRequest();

                _manager.CityService.CreateOneCity(city);


                return StatusCode(201, city);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/City/DeleteOneCity/{id:int}")]
        public IActionResult DeleteOneCity([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.CityService.DeleteOneCity(id, false);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the city.");
            }
        }
    }
}
