using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Services.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }

    public class Message
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(Message message)
        {
            using (var smtpClient = new SmtpClient
            {
                Host = _configuration["Smtp:Host"],
                Port = int.Parse(_configuration["Smtp:Port"]),
                EnableSsl = bool.Parse(_configuration["Smtp:EnableSsl"]),
                Credentials = new NetworkCredential(_configuration["Smtp:Email"], _configuration["Smtp:Password"])
            })
            {
                using (var mailMessage = new MailMessage(_configuration["Smtp:Email"], message.To)
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                })
                {
                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                        Console.WriteLine("E-posta başarıyla gönderildi.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("E-posta gönderme hatası: " + ex.Message);
                    }
                }
            }
        }
    }
}
