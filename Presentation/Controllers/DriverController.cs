using Entitiess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Presentation.Controllers
{
    public class DriverController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly RepositoryContext _context;
        public DriverController(IServiceManager manager, RepositoryContext context)
        {
            _manager = manager;
            _context = context;
        }

        [HttpGet("api/Driver/GetAllDriver")]
        public IActionResult GetAllDriver()
        {
            var drivers = _manager.DriverService.GetAllDriver(false);
            return Ok(drivers);
        }

        [HttpGet("api/Driver/GetOneDriverById/{id:int}")]
        public IActionResult GetOneDriverById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var drivers = _manager
                .DriverService
                .GetOneDriverById(id, false);


                if (drivers is null)
                    return NotFound();

                return Ok(drivers);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/CheckEmailExistence")]
        public ActionResult CheckEmailExistence(string email)
        {
            var driver = _context.Driver.FirstOrDefault(u => u.Details.Email == email);
            if (driver != null)
            {
                return Ok(new { exists = true });
            }
            else
            {
                return Ok(new { exists = false });
            }
        }

        [HttpPost("api/Driver/CreateOneDriver")]
        public IActionResult CreateOneDriver([FromBody] Entitiess.Models.Driver drivers)
        {
            try
            {
                if (drivers is null)
                    return BadRequest();
                _manager.DriverService.CreateOneDriver(drivers);


                return Ok("İşlem Başarılı");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/Driver/DeleteOneDriver/{id:int}")]
        public IActionResult DeleteOneDriver([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.DriverService.DeleteOneDriver(id, false);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("api/Driver/DriverLogin")]
        public IActionResult DriverLogin([FromBody] LoginRequest loginRequest)
        {
            var authentication = _manager.DriverService.AuthenticateDriver(loginRequest);
            if (authentication.Item1)
            {
                return Ok(new { jwtToken = authentication.Item2 });
            }
            else
            {
                return Unauthorized(authentication.Item2);
            }
        }

        private ActionResult Json(object value)
        {
            var json = JsonConvert.SerializeObject(value);
            return new ContentResult
            {
                Content = json,
                ContentType = "application/json"
            };
        }
    }
}