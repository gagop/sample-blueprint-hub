namespace Gakko.Application.Interfaces;

public interface IEmailService
{
    Task SendEmail(string email, string title, string message);
}