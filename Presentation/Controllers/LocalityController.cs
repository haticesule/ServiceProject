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
    public class LocalityController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public LocalityController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("api/Locality/GetAllLocality")]
        public IActionResult GetAllLocality()
        {
            var locality = _manager.LocalityService.GetAllLocality(false);
            return Ok(locality);
        }

        [HttpGet("api/Locality/GetOneLocalityById/{id:int}")]
        public IActionResult GetOneLocalityById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var locality = _manager
                .LocalityService
                .GetOneLocalityById(id, false);


                if (locality is null)
                    return NotFound();

                return Ok(locality);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        [HttpGet("api/Locality/GetLocalityByTownId/{id:int}")]
        public IActionResult GetLocalityByTownId([FromRoute(Name = "id")] int id)
        {
            try
            {
                var locality = _manager
                .LocalityService
                .GetLocalityByTownId(id);

                if (locality is null)
                    return NotFound();

                return Ok(locality);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("api/Locality/CreateOneLocality")]
        public IActionResult CreateOneLocality([FromBody] Entitiess.Models.Locality locality)
        {
            try
            {
                if (locality is null)
                    return BadRequest();

                _manager.LocalityService.CreateOneLocality(locality);


                return StatusCode(201, locality);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/Locality/DeleteOneLocality/{id:int}")]
        public IActionResult DeleteOneLocality([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.LocalityService.DeleteOneLocality(id, false);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
