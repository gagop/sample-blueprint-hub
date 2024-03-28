namespace Gakko.API.Services;

/// <summary>
///     Simulating communication through the external email service
/// </summary>
public class EmailService : IEmailService
{
    public async Task SendEmail(string email, string title, string message)
    {
        // Simulating sending an email
        // In a real-world scenario, this would be a call to an external service
        var randomWaitTime = new Random().Next(500, 1000);
        await Task.Delay(randomWaitTime);
    }
}