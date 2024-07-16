using Microsoft.Extensions.Configuration;
using Services.Contracts;
using System.Net.Mail;
using System.Net;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(Message message)
    {
        var smtpClient = new SmtpClient
        {
            Host = _configuration["EmailSettings:SmtpServer"],
            UseDefaultCredentials = false,
            Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),
            EnableSsl = true,
            Credentials = new NetworkCredential(
                _configuration["EmailSettings:SenderEmail"],
                _configuration["EmailSettings:SenderPassword"]
            )
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderName"]),
            Subject = message.Subject,
            Body = message.Body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(message.To);

        await smtpClient.SendMailAsync(mailMessage);
    }
}
