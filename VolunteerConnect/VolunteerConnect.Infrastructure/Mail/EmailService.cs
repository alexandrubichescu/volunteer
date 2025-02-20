using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VolunteerConnect.Application.Contracts.Infrastructure;
using VolunteerConnect.Application.Models.Mail;

namespace VolunteerConnect.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmail(Email email)
        {

            string fromMail = _emailSettings.FromAddress;
            string fromPassword = _emailSettings.ApiKey;

            MailMessage message = new MailMessage
            {
                From = new MailAddress(fromMail),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email.To));

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true
            };

            try
            {
                smtpClient.Send(message);
                _logger.LogInformation("Email sent successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }

             return true;

         
        }
    }
}
