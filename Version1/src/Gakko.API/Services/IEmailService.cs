namespace Gakko.API.Services;

public interface IEmailService
{
    Task SendEmail(string email, string title, string message);
}