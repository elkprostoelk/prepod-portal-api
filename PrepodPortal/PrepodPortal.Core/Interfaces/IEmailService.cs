namespace PrepodPortal.Core.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string name, string email, string password);
}