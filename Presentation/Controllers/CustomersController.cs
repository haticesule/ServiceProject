using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    
    public class CustomersController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public CustomersController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("api/Customers/GetAllCustomer")]
        public IActionResult GetAllCustomer()
        {
            var customers = _manager.CustomerService.GetAllCustomer(false);
            return Ok(customers);
        }

        [HttpGet("api/Customers/GetOneCustomerById/{id:int}")]
        public IActionResult GetOneCustomerById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var customer = _manager
                .CustomerService
                .GetOneCustomerById(id, false);


                if (customer is null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost("api/Customers/CreateOneCustomer")]
        public IActionResult CreateOneCustomer([FromBody] Entitiess.Models.Customer customer)
        {
            try
            {
                if (customer is null)
                    return BadRequest();

                _manager.CustomerService.CreateOneCustomer(customer);


                return StatusCode(201, customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/Customers/DeleteOneCustomer/{id:int}")]
        public IActionResult DeleteOneCustomer([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.CustomerService.DeleteOneCustomer(id, false);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
