using Entitiess.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class RequestController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IEmailSender _emailSender;
        public RequestController(IServiceManager manager, IEmailSender emailSender)
        {
            _manager = manager;
            _emailSender = emailSender; 
        }
        [HttpGet("api/Request/GetAllRequest")]
        public IActionResult GetAllRequest()
        {
            var request = _manager.RequestService.GetAllRequest(false);
            return Ok(request);
        }

        [HttpGet("api/Request/GetOneRequestById/{id:int}")]
        public IActionResult GetOneRequestById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var request = _manager
                .RequestService
                .GetOneRequestById(id, false);


                if (request is null)
                    return NotFound();

                return Ok(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("api/Request/CreateOneRequest")]
        public IActionResult CreateOneRequest([FromBody] Entitiess.Models.Request request)
        {
            try
            {
                if (request is null)
                    return BadRequest();

                _manager.RequestService.CreateOneRequest(request);


                return StatusCode(201, request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/Request/DeleteOneRequest/{id:int}")]
        public IActionResult DeleteOneRequest([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.RequestService.DeleteOneRequest(id, false);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the request.");
            }
        }

        [HttpPost("api/Request/Confirm")]
        public async Task<IActionResult> ConfirmReservation([FromBody] Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await SendConfirmationEmail(request);

            return result ? Ok(new { success = true }) : StatusCode(500, "İşlem sırasında bir hata oluştu.");
        }

        private async Task<bool> SendConfirmationEmail(Request request)
        {
           
            Console.WriteLine("SendConfirmationEmail fonksiyonu çağrıldı.");

            var r = _manager.RequestService.GetOneRequestById(request.RequestId, true);
            if (r != null && r.Any() && r[0] != null && r[0].Customer != null && r[0].Customer.Details != null)
            {
                var email = r[0].Customer.Details.Email;
                if (string.IsNullOrEmpty(email))
                {
                    Console.WriteLine("E-posta adresi eksik.");
                    return false;
                }

                var message = new Message
                {
                    To = email,
                    Subject = "Rezervasyon Onayı",
                    Body = $"Sayın {r[0].Customer.Details.Name},\n\n" +
                           $"Rezervasyonunuz başarıyla onaylanmıştır.\n\n" +
                           $"Rezervasyon detayları:\n" +
                           $"- Rezervasyon Tarihi: {r[0].BookingDate.ToString()}\n" +
                           $"- Rezervasyon Saati: {r[0].BookingTime.ToString()}\n" +
                           $"- Kalkış Yeri: {r[0].Neighborhood.NeighborhoodName.ToString()}\n" +
                           $"- Varış Yeri: {r[0].DestinationNeighborhood.ToString()}\n\n" +
                           "Teşekkürler"
                };

                await _emailSender.SendEmailAsync(message);
                Console.WriteLine("E-posta gönderildi.");
                return true;
            }
            else
            {
                Console.WriteLine("Gerekli veriler eksik.");
                return false;
            }
        }

    }
}

