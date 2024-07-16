using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class TownController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public TownController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("api/Town/GetAllTown")]
        public IActionResult GetAllTown()
        {
            var town = _manager.TownService.GetAllTown(false);
            return Ok(town);
        }

        [HttpGet("api/Town/GetOneTownById/{id:int}")]
        public IActionResult GetOneTownById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var town = _manager
                .TownService
                .GetOneTownById(id, false);


                if (town is null)
                    return NotFound();

                return Ok(town);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet("api/Town/GetOneTCityById/{id:int}")]
        public IActionResult GetOneTCityById([FromRoute(Name ="id")] int id)
        {
            try
            {
                var town = _manager
                .TownService
                .GetOneTCityById(id);

                if (town is null)
                    return NotFound();

                return Ok(town);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost("api/Town/CreateOneTown")]
        public IActionResult CreateOneTown([FromBody] Entitiess.Models.Town town)
        {
            try
            {
                if (town is null)
                    return BadRequest();

                _manager.TownService.CreateOneTown(town);


                return StatusCode(201, town);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/Town/DeleteOneTown/{id:int}")]
        public IActionResult DeleteOneTown([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.TownService.DeleteOneTown(id, false);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
