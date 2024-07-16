using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class DetailsController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public DetailsController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet("api/Details/GetAllDetails")]
        public IActionResult GetAllDetails()
        {
            var details = _manager.DetailsService.GetAllDetails(false);
            return Ok(details);
        }

        [HttpGet("api/Details/GetOneDetailsById/{id:int}")]
        public IActionResult GetOneDetailsById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var details = _manager
                .DetailsService
                .GetOneDetailsById(id, false);


                if (details is null)
                    return NotFound();

                return Ok(details);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("api/Details/CreateOneDetails")]
        public IActionResult CreateOneDetails([FromBody] Entitiess.Models.Details details)
        {
            try
            {
                if (details is null)
                    return BadRequest();

                _manager.DetailsService.CreateOneDetails(details);


                return StatusCode(201, details);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/Details/DeleteOneDetails/{id:int}")]
        public IActionResult DeleteOneDetails([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.DetailsService.DeleteOneDetails(id, false);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the details."); 
            }
        }
    }
}
