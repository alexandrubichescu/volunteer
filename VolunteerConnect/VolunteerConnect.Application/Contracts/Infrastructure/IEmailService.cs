using VolunteerConnect.Application.Models.Mail;

namespace VolunteerConnect.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}
