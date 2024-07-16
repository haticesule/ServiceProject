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
    public class AdminController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public AdminController (IServiceManager manager)
        {
            _manager = manager;
        }
        
        [HttpPost("api/Admin/register")]
        public IActionResult Register([FromBody] Admin admin)
        {
            return Ok(_manager.AdminService.CreateOneAdmin(admin));
        }

        [HttpPost("api/Admin/login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var authentication = _manager.AdminService.AuthenticateAdmin(loginRequest);
            if(authentication.Item1)
            {
                return Ok(new {jwtToken = authentication.Item2});
            }
            else
            {
                return Unauthorized(authentication.Item2);
            }
        }

        [HttpGet("api/Admin/GetAllAdmin")]
        public IActionResult GetAllAdmin()
        {
            var admin = _manager.AdminService.GetAllAdmin(false);
            return Ok(admin);
        }
    }
}
